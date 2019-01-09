using System;
using System.Drawing;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.Director
{
    /// <summary>
    /// Класс формы расчета стоимости простоя сервиса
    /// </summary>
    public partial class EditIdleItemForm : Form
    {
        // Минут в году
        private const int minutesInYear = 525600;

        // Редактируемый экземпляр простоя
        private ServiceIdle _serviceIdle;

        // Кол-во отключенных пользователей
        private int _disabledUsersCount;
        // Кол-во задействованных администраторов
        private int _usedAdminsCount;
        // Продолжительность (мин.)
        private double _duration;
        // Плановые расходы на администраторов
        private decimal _adminPlannedCost;
        // Плановые расходы на конечных пользователей
        private decimal _employeePlannedCost;
        // Плановые расходы на отключение сервера
        private decimal _servicePlannedCost;
        // Потерянный доход 
        private decimal _brokenIncome;
        // Итоговая сумма простоя
        private decimal _total;

        // Доход в минуту
        private decimal _incomeInMinute;
        // Средний доход администратора в минуту
        private decimal _avgAdminSalaryPerMin;
        // Средний доход работника в минуту
        private decimal _avgEmployeeSalaryPerMin;
        // Доход на каждого работника (руб./мин)
        private decimal _incomePerEmployee;

        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Конструктор формы расчета стоимости простоя сервиса
        /// </summary>
        /// <param name="selectedServiceIdle">Выбранный экземпляр простоя</param>
        public EditIdleItemForm(ServiceIdle selectedServiceIdle)
        {
            _serviceIdle = selectedServiceIdle;
            InitializeComponent();

            _ctx = new Context();

            btn_OK.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));
            InitBindings(selectedServiceIdle);
            comboBox_DurationType.SelectedIndex = 0;
        }

        /// <summary>
        /// Инициализация привязок
        /// </summary>
        private void InitBindings(ServiceIdle serviceIdle)
        {
            serviceIdleBindingSource.DataSource = serviceIdle;
            serviceIdleBindingSource.ResumeBinding();

            // длительность простоя
            _duration = (serviceIdle.EndPeriod - serviceIdle.BeginPeriod).TotalMinutes;
            // количество пользователей, отключенных при этом
            _disabledUsersCount = serviceIdle.Service.User.Count;
            // количество администраторов, задействованных для этого
            _usedAdminsCount = serviceIdle.User.Count;

            // Годовой доход компанни
            CostIdleParams idleParams = null;
            try
            {
                // Попытка загрузки из файла
                idleParams = CostIdleParams.Load();
                // При отсутсвтсвии файла данные берутся из БД
                if (idleParams == null)
                    idleParams = _ctx.GetIdleParamsFormDatabase();
            }
            catch
            {
                // в случае ошибки данные берутся из БД
                idleParams = _ctx.GetIdleParamsFormDatabase();
            }
            finally
            {
                costIdleParamsBindingSource.DataSource = idleParams;
                costIdleParamsBindingSource.ResumeBinding();
            }

            CalcIdleCost();
        }

        /// <summary>
        /// Расчет стоимости простоя
        /// </summary>
        private void CalcIdleCost()
        {
            var idleParams = costIdleParamsBindingSource.DataSource as CostIdleParams;
            if (idleParams == null)
                return;

            // Доход в минуту
            _incomeInMinute = idleParams.YearIncome / minutesInYear;
            // Средний доход администратора в минуту
            _avgAdminSalaryPerMin = idleParams.AvgAdminSalary / 60;
            // Средний доход работника в минуту
            _avgEmployeeSalaryPerMin = idleParams.AvgEmployeeSalary / 60;
            // Доход на каждого работника (руб./мин)
            _incomePerEmployee = _incomeInMinute / idleParams.TotalEmployeeCount;

            // Плановые расходы на администраторов (руб./мин.)
            _adminPlannedCost = (decimal)_duration * _usedAdminsCount * _avgAdminSalaryPerMin;
            // Плановые расходы на конечных пользователей (руб./мин.)
            _employeePlannedCost = (decimal)_duration * _disabledUsersCount * _avgEmployeeSalaryPerMin;
            // Плановые расходы на отключение сервера
            _servicePlannedCost = _adminPlannedCost + _employeePlannedCost;
            // Потерянный доход (руб./мин.)
            _brokenIncome = _incomePerEmployee * (decimal)_duration;

            // Итоговая стоимость простоя
            _total = _servicePlannedCost + _brokenIncome;

            UpdateTextFields();
        }

        /// <summary>
        /// Обновление текстовых полей
        /// </summary>
        private void UpdateTextFields()
        {
            txt_ServiceName.Text = _serviceIdle.Service.ServiceType.Name;
            txt_PlarformName.Text = _serviceIdle.Service.PaasType.Name;
            txt_IdleTypeName.Text = _serviceIdle.IdleType.Name;
            txt_IdleReasonName.Text = _serviceIdle.IdleReason.Name;

            switch (comboBox_DurationType.SelectedIndex)
            {
                case 0:
                    txt_Duration.Text = _duration.ToString();
                    break;
                case 1:
                    var hours = Math.Round(_duration / 60);
                    var mins = Math.Round(_duration % 60);
                    txt_Duration.Text = $"{hours:##}:{(mins > 0 ? $"{mins:##}": "00")}";
                    break;
            }


            txt_IncomePerMinute.Text = _incomeInMinute.ToString("C");
            txt_incomePerEmployee.Text = _incomePerEmployee.ToString("C");
            txt_disabledUsersCount.Text = _disabledUsersCount.ToString();
            txt_usedAdminsCount.Text = _usedAdminsCount.ToString();
            txt_servicePlannedCost.Text = _servicePlannedCost.ToString("C");
            txt_brokenIncome.Text = _brokenIncome.ToString("C");
            txt_Total.Text = _total.ToString("C");
        }

        /// <summary>
        /// Изменение значения полей
        /// </summary>
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox9.Text))
                CalcIdleCost();
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в списке параметров простоя
        /// </summary>
        private void costIdleParamsBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            CalcIdleCost();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который осуществляет отмену изменений в параметрах простоя, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который осуществляет сохранение изменений в параметрах простоя
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            var costIdleParams = costIdleParamsBindingSource.DataSource as CostIdleParams;
            if (costIdleParams == null)
            {
                MessageBox.Show("Не удалось получить параметры для расчета простоя!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (costIdleParams.YearIncome <= 0)
            {
                MessageBox.Show("Введите годовой валовой доход компании для расчета стоимости простоя!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (costIdleParams.TotalEmployeeCount <= 0)
            {
                MessageBox.Show("Введите общее число сотрудников для расчета стоимости простоя!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (costIdleParams.TotalEmployeeCount < (_disabledUsersCount + _usedAdminsCount))
            {
                MessageBox.Show("Общее число сотрудников должно быть больше количества отключенных и задействованных!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (costIdleParams.AvgAdminSalary <= 0)
            {
                MessageBox.Show("Введите размер средней часовой оплаты труда администратора для расчета стоимости простоя!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (costIdleParams.AvgEmployeeSalary <= 0)
            {
                MessageBox.Show("Введите размер средней часовой оплаты труда работника для расчета стоимости простоя!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (_total <= 0)
            {
                MessageBox.Show("Не расчитана итоговая стоимость простоя!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Полученная стоимость будет сохранена! Продолжить?", "Сохранение изменений",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.No)
                return;

            try
            {
                CostIdleParams.Save(costIdleParams);
            }
            catch
            {
                result = MessageBox.Show("Не удалось сохранить параметры для расчета! " +
                                         "\nПри новом расчете простоя придется снова ввести одовой валовой доход компании!" +
                                         "\nПродолжить сохранение?", "Ошибка", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Error);
                if (result == DialogResult.No)
                    return;
            }

            try
            {
                _serviceIdle.Cost = _total;
                _ctx.UpdateServiceIdle(_serviceIdle);
                DialogResult = DialogResult.OK;
                MessageBox.Show("Полученный расчет простоя успешно сохранен!", "Информация", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Close();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить полученные расчеты!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события изменения индекса формата отображения времени
        /// </summary>
        private void comboBox_DurationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTextFields();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы распределения задействованного персонала
        /// </summary>
        private void распределениеПерсоналаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var staffDistributonForm = new StaffDistributonForm(_serviceIdle);
            if (staffDistributonForm.ShowDialog() == DialogResult.OK)
            {
                DialogResult = DialogResult.OK;
                _serviceIdle = staffDistributonForm.SelectedServiceIdle;
                // количество администраторов, задействованных для этого
                _usedAdminsCount = _serviceIdle.User.Count;
                CalcIdleCost();
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который открывает диалоговое окно предварительного просмотра отчета по текущему простою
        /// </summary>
        private void btn_PrintReport_Click(object sender, EventArgs e)
        {
            var costIdleParams = costIdleParamsBindingSource.DataSource as CostIdleParams;
            if (costIdleParams == null)
            {
                MessageBox.Show("Не удалось получить параметры для расчета простоя!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (costIdleParams.YearIncome <= 0)
            {
                MessageBox.Show("Введите годовой валовой доход компании для расчета стоимости простоя!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (costIdleParams.TotalEmployeeCount <= 0)
            {
                MessageBox.Show("Введите общее число сотрудников для расчета стоимости простоя!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (costIdleParams.TotalEmployeeCount < (_disabledUsersCount + _usedAdminsCount))
            {
                MessageBox.Show("Общее число сотрудников должно быть больше количества отключенных и задействованных!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (costIdleParams.AvgAdminSalary <= 0)
            {
                MessageBox.Show("Введите размер средней часовой оплаты труда администратора для расчета стоимости простоя!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (costIdleParams.AvgEmployeeSalary <= 0)
            {
                MessageBox.Show("Введите размер средней часовой оплаты труда работника для расчета стоимости простоя!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (_total <= 0)
            {
                MessageBox.Show("Не расчитана итоговая стоимость простоя!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var sip = new ServiceIdleParams();
            sip.Id = _serviceIdle.Id;
            sip.Service = _serviceIdle.Service.ServiceType.Name;
            sip.Platform = _serviceIdle.Service.PaasType.Name;
            sip.Begin = _serviceIdle.BeginPeriod;
            sip.End = _serviceIdle.EndPeriod;
            sip.DurationType = comboBox_DurationType.Text;
            sip.DurationValue = txt_Duration.Text;
            sip.IdleType = _serviceIdle.IdleType.Name;
            sip.IdleReason = _serviceIdle.IdleReason.Name;
            sip.DisabledEmployees = _disabledUsersCount;
            sip.UsedEmployees = _disabledUsersCount;
            sip.ServerCost = _servicePlannedCost;
            sip.LostCost = _brokenIncome;
            sip.TotalCost = _total;

            var reportingForm = new ReportingForm(sip);
            reportingForm.ShowDialog();
        }
    }
}
