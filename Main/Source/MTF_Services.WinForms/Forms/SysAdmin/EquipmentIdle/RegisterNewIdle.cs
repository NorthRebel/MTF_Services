using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.EquipmentIdle
{
    /// <summary>
    /// Класс формы регистрации новго простоя
    /// </summary>
    public partial class RegisterNewIdle : Form
    {
        private readonly SAN _san;
        private readonly Server _server;

        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Дата начала обслуживания
        /// </summary>
        private DateTime _beginDateTime;

        /// <summary>
        /// Дата окончания обслуживания
        /// </summary>
        private DateTime _endDateTime;

        /// <summary>
        /// Доступный персонал
        /// </summary>
        public BindingList<User> AvaliblePersonal { get; set; }

        /// <summary>
        /// Задействованный персонал
        /// </summary>
        public BindingList<User> UsedPersonal { get; set; }

        /// <summary>
        /// Выбранный доступный сотрудник
        /// </summary>
        public User SelectedAvalibleUser { get; set; }

        /// <summary>
        /// Выбранный задействованный сотрудник
        /// </summary>
        public User SelectedUsedUser { get; set; }

        /// <summary>
        /// Выбранный тип простоя
        /// </summary>
        public IdleType SelectedIdleType { get; set; }

        /// <summary>
        /// Выбранная причина простоя
        /// </summary>
        public IdleReason SelectedIdleReason { get; set; }
        
        /// <summary>
        /// Конструктор формы регистрации нового простоя для сервера
        /// </summary>
        public RegisterNewIdle(Server server)
        {
            _server = server;
            InitializeComponent();

            _ctx = new Context();

            btn_OK.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));

            comboBox_DurationType.SelectedIndex = 0;
            SetEndDate();
            BindCollections();
        }

        /// <summary>
        /// Конструктор формы регистрации нового простоя для хранилища данных
        /// </summary>
        public RegisterNewIdle(SAN san)
        {
            _san = san;
            InitializeComponent();

            _ctx = new Context();

            btn_OK.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));

            comboBox_DurationType.SelectedIndex = 0;
            SetEndDate();
            BindCollections();
        }

        /// <summary>
        /// Привязка коллекций
        /// </summary>
        private void BindCollections()
        {
            idleTypeBindingSource.DataSource = _ctx.GetIdleTypes();
            idleReasonBindingSource.DataSource = _ctx.GetIdleReasons();

            AvaliblePersonal = new BindingList<User>(_ctx.GetAdminsList());
            UsedPersonal = new BindingList<User>();

            AvalibleUserBindingSource.DataSource = AvaliblePersonal;
            UsedUserBindingSource.DataSource = UsedPersonal;

            dg_AvalibleUser.DataSource = AvalibleUserBindingSource;
            dg_UsedUser.DataSource = UsedPersonal;
        }

        /// <summary>
        /// Обработчик события изменения даты начала обслуживания,
        /// который осуществляет корректировку допустимой даты окончания обслуживания и расчет длительности
        /// </summary>
        private void dateTimePicker_Begin_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown_BeginHours.Value == 23 && numericUpDown_BeginMinutes.Value == 59)
                dateTimePicker_End.MinDate = dateTimePicker_Begin.Value.AddDays(1);
            else
                SetEndDate();
        }

        /// <summary>
        /// Корректировка допустимой даты
        /// </summary>
        private void SetEndDate()
        {
            if (numericUpDown_BeginHours.Value == 23 && numericUpDown_BeginMinutes.Value == 59)
                dateTimePicker_End.MinDate = dateTimePicker_Begin.Value.AddDays(1);
            else
            {
                dateTimePicker_End.MinDate = dateTimePicker_Begin.Value;

                if (numericUpDown_BeginMinutes.Value == 59)
                {
                    numericUpDown_EndMinutes.Minimum = 0;
                    numericUpDown_EndHours.Minimum = numericUpDown_BeginHours.Value + 1;
                }
                else if (dateTimePicker_End.Value > dateTimePicker_Begin.Value)
                {
                    numericUpDown_EndHours.Minimum = 0;
                    numericUpDown_EndMinutes.Minimum = 0;
                }
                else
                {
                    if (numericUpDown_EndHours.Value == numericUpDown_BeginHours.Value)
                        numericUpDown_EndMinutes.Minimum = numericUpDown_BeginMinutes.Value;
                    numericUpDown_EndHours.Minimum = numericUpDown_BeginHours.Value;
                }
            }

            CalculateDuration();
        }

        /// <summary>
        /// Расчет разницы между окончанием и началом обслуживания
        /// </summary>
        private void CalculateDuration()
        {
            _beginDateTime = new DateTime
            (
                dateTimePicker_Begin.Value.Year,
                dateTimePicker_Begin.Value.Month,
                dateTimePicker_Begin.Value.Day,
                (int)numericUpDown_BeginHours.Value,
                (int)numericUpDown_BeginMinutes.Value,
                0
            );

            _endDateTime = new DateTime
            (
                dateTimePicker_End.Value.Year,
                dateTimePicker_End.Value.Month,
                dateTimePicker_End.Value.Day,
                (int)numericUpDown_EndHours.Value,
                (int)numericUpDown_EndMinutes.Value,
                0
            );

            TimeSpan difference = _endDateTime - _beginDateTime;

            switch (comboBox_DurationType.SelectedIndex)
            {
                case 0:
                    textBox_Duration.Text = difference.TotalMinutes.ToString();
                    break;
                case 1:
                    textBox_Duration.Text = TimeSpan.FromHours(difference.TotalHours).ToString(@"hh\:mm");
                    break;
            }
        }

        /// <summary>
        /// Обработчик события изменения часа начала обслуживания,
        /// который осуществляет корректировку допустимой даты окончания обслуживания и расчет длительности
        /// </summary>
        private void numericUpDown_BeginHours_ValueChanged(object sender, EventArgs e)
        {
            SetEndDate();
        }

        /// <summary>
        /// Обработчик события изменения минуты начала обслуживания,
        /// который осуществляет корректировку допустимой даты окончания обслуживания и расчет длительности
        /// </summary>
        private void numericUpDown_BeginMinutes_ValueChanged(object sender, EventArgs e)
        {
            SetEndDate();
        }

        /// <summary>
        /// Обработчик события изменения даты окончания обслуживания,
        /// который осуществляет корректировку допустимой даты окончания обслуживания и расчет длительности
        /// </summary>
        private void dateTimePicker_End_ValueChanged(object sender, EventArgs e)
        {
            SetEndDate();
        }

        /// <summary>
        /// Обработчик события изменения часа окончания обслуживания,
        /// который осуществляет корректировку допустимой даты окончания обслуживания и расчет длительности
        /// </summary>
        private void numericUpDown_EndHours_ValueChanged(object sender, EventArgs e)
        {
            SetEndDate();
        }

        /// <summary>
        /// Обработчик события изменения минуты окончания обслуживания,
        /// который осуществляет корректировку допустимой даты окончания обслуживания и расчет длительности
        /// </summary>
        private void numericUpDown_EndMinutes_ValueChanged(object sender, EventArgs e)
        {
            SetEndDate();
        }

        /// <summary>
        /// Обработчик события изменения индекса формата отображения длительности обслуживания
        /// </summary>
        private void comboBox_DurationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_Duration.Text))
                return;

            double result = 0;
            var tryParse = double.TryParse(textBox_Duration.Text, out result);

            switch (comboBox_DurationType.SelectedIndex)
            {
                case 0:
                    if (tryParse)
                        textBox_Duration.Text = (result * 60).ToString();
                    else
                    {
                        string[] strings = textBox_Duration.Text.Split(':');
                        var hours = int.Parse(strings[0]);
                        var minutes = int.Parse(strings[1]);
                        var timeSpan = new TimeSpan(hours, minutes, 0);
                        textBox_Duration.Text = textBox_Duration.Text = timeSpan.TotalMinutes.ToString();
                    }
                    break;
                case 1:
                    if (tryParse)
                        textBox_Duration.Text = TimeSpan.FromHours(TimeSpan.FromMinutes(result).TotalHours)
                            .ToString(@"hh\:mm");
                    break;
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных доступного персонала
        /// </summary>
        private void AvalibleUserBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as User;
            if (selectedItem != null)
                SelectedAvalibleUser = selectedItem;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных задействованного персонала
        /// </summary>
        private void UsedUserBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as User;
            if (selectedItem != null)
                SelectedUsedUser = selectedItem;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных типа простоя
        /// </summary>
        private void idleTypeBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as IdleType;
            if (selectedItem != null)
                SelectedIdleType = selectedItem;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных причины простоя
        /// </summary>
        private void idleReasonBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as IdleReason;
            if (selectedItem != null)
                SelectedIdleReason = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит удаление выбранного сотрудника из задействованных сотрудников
        /// </summary>
        private void btn_RemoveFromUsed_Click(object sender, EventArgs e)
        {
            if (SelectedUsedUser == null || dg_UsedUser.CurrentRow == null)
            {
                MessageBox.Show("Пользователь не выбран!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            AvaliblePersonal.Add(SelectedUsedUser);
            UsedPersonal.Remove(SelectedUsedUser);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит добавление выбранного сотрудника из задействованных сотрудников
        /// </summary>
        private void btn_AddToUsed_Click(object sender, EventArgs e)
        {
            if (SelectedAvalibleUser == null || dg_AvalibleUser.CurrentRow == null)
            {
                MessageBox.Show("Пользователь не выбран!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            UsedPersonal.Add(SelectedAvalibleUser);
            AvaliblePersonal.Remove(SelectedAvalibleUser);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который выполняет сохранение изменений
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (_beginDateTime >= _endDateTime)
            {
                MessageBox.Show("Конечная дата должна быть больше начальной!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (_endDateTime >= DateTime.Now)
            {
                MessageBox.Show("Конечная дата должна быть меньше текущей!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (SelectedIdleType == null || comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите тип простоя!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (SelectedIdleReason == null || comboBox2.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите причину простоя!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (UsedPersonal.Count == 0)
            {
                MessageBox.Show("Добавьте к задействованному персоналу сотрудников", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                List<Service> services = null;
                if (_server != null)
                    services = _ctx.GetServicesByServer(_server);
                else
                    services = _ctx.GetServicesBySAN(_san);

                _ctx.RegisterNewIdle(services, UsedPersonal.ToList(), _beginDateTime, _endDateTime,
                    SelectedIdleType, SelectedIdleReason);
                MessageBox.Show("Простой оборудования успешно зарегистрирован!", "Информация", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при регистрации нового простоя! Изменения не сохранены!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену регистрации простоя, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
