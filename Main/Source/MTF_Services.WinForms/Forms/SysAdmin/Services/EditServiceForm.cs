using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;
using MTF_Services.WinForms.Forms.SysAdmin.Services.Dictionary;
using MTF_Services.WinForms.Forms.SysAdmin.Services.Parts;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Services
{
    /// <summary>
    /// Класс формы редактирования сервиса
    /// </summary>
    public partial class EditServiceForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Режим работы формы.
        /// </summary>
        private FormMode _formMode;

        /// <summary>
        /// Редактируемый сервис
        /// </summary>
        public Service CurrentService { get; set; }

        /// <summary>
        /// Информация о стоимости компонентов сервиса
        /// </summary>
        public ServicePartsInfo ServicePartsInfo { get; set; }

        /// <summary>
        /// Выбранное программное обеспечение
        /// </summary>
        public List<SoftwareInfo> SelectedSoftware { get; set; }

        /// <summary>
        /// Конструктор формы создания нового сервиса
        /// </summary>
        public EditServiceForm()
        {
            InitializeComponent();
            btn_Save.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));

            _ctx = new Context();
            CurrentService = new Service();
            BindServerPartsInfo();
            BindPlatforms();
            BindServiceTypes();
            BindSelectedSoftware();

            _formMode = FormMode.Add;
            Text = "Создание нового сервиса";
        }

        /// <summary>
        /// Конструктор формы создания нового сервиса с выбранной платформой
        /// </summary>
        /// <param name="selectedPlatform"></param>
        public EditServiceForm(PaasType selectedPlatform) : this()
        {
            int selectedIndex = ((BindingList<PaasType>)paasTypeBindingSource.DataSource).IndexOf(selectedPlatform);
            if (selectedIndex > -1)
            {
                paasTypeBindingSource.Position = selectedIndex;
                comboBox_PaasTypes.SelectedIndex = selectedIndex;
            }
        }

        /// <summary>
        /// Конструктор формы редактирования выбранного сервиса
        /// </summary>
        /// <param name="selectedService">Выбранный сервис</param>
        public EditServiceForm(Service selectedService) 
        {
            InitializeComponent();
            btn_Save.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));

            _ctx = new Context();
            CurrentService = selectedService;
            PrepareBindingsToEditService();
            BindServerPartsInfo();
            BindPlatforms();
            BindServiceTypes();
            BindSelectedSoftware();

            _formMode = FormMode.Edit;
            Text = "Редактирование сервиса";
        }

        /// <summary>
        /// Инициализация привязок для редактирования конфигурации сервиса
        /// </summary>
        private void PrepareBindingsToEditService()
        {
            serviceBindingSource.DataSource = CurrentService;
            serviceBindingSource.ResumeBinding();
            paasTypeBindingSource_CurrentChanged(paasTypeBindingSource, EventArgs.Empty);

            foreach (var software in CurrentService.Software)
            {
                SelectedSoftware.Add(new SoftwareInfo
                {
                    Id = software.Id,
                    Software = software.Name,
                    SoftType = software.SoftType.Name,
                    Cost = software.Cost
                });
            }

            UpdateServerPartsInfo();
        }

        #region Bindings

        /// <summary>
        /// Инициализация привязки платформ
        /// </summary>
        private void BindPlatforms()
        {
            paasTypeBindingSource.DataSource = _ctx.GetPaasTypes();
            comboBox_PaasTypes.DataSource = paasTypeBindingSource;
        }

        /// <summary>
        /// Инициализация привязки типов сервиса
        /// </summary>
        private void BindServiceTypes()
        {
            serviceTypeBindingSource.DataSource = _ctx.GetServiceTypes();
            comboBox_ServiceTypes.DataSource = serviceTypeBindingSource;
        }

        /// <summary>
        /// Инициализация привязки информации о стоимости компонентов сервиса
        /// </summary>
        private void BindServerPartsInfo()
        {
            ServicePartsInfo = new ServicePartsInfo();
            servicePartsInfoBindingSource.DataSource = ServicePartsInfo;
        }

        /// <summary>
        /// Обновление привязки информации о стоимости компонентов сервиса
        /// </summary>
        private void UpdateServerPartsInfo()
        {
            servicePartsInfoBindingSource.DataSource = ServicePartsInfo;
            servicePartsInfoBindingSource.ResetBindings(true);

            CurrentService.CostPerHour = ServicePartsInfo.FinalPrice;
            serviceBindingSource.DataSource = CurrentService;
            serviceBindingSource.ResetBindings(false);
        }

        /// <summary>
        /// Привязка списка выбранного программного обеспечения
        /// </summary>
        private void BindSelectedSoftware()
        {
            SelectedSoftware = new List<SoftwareInfo>();
            softwareInfoBindingSource.DataSource = new BindingList<SoftwareInfo>(SelectedSoftware);
            dg_Software.DataSource = softwareInfoBindingSource;
            dg_Software.Refresh();
        }

        /// <summary>
        /// Обновление привязки списка выбранного программного обеспечения
        /// </summary>
        private void UpdateSelectedSoftware()
        {
            softwareInfoBindingSource.DataSource = new BindingList<SoftwareInfo>(SelectedSoftware);
            dg_Software.DataSource = softwareInfoBindingSource;
            dg_Software.Refresh();
        }

        #endregion

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных типов платформ
        /// </summary>
        private void paasTypeBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var currentPaasType = paasTypeBindingSource.Current as PaasType;

            if (currentPaasType != null)
            {
                platformServerItemBindingSource.DataSource = _ctx.GetServersByPlatform(currentPaasType);
                comboBox_Servers.DataSource = platformServerItemBindingSource;

                platfromSANItemBindingSource.DataSource = _ctx.GetSANsByPlatform(currentPaasType);
                comboBox_sans.DataSource = platfromSANItemBindingSource;
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных серверов
        /// </summary>
        private void platformServerItemBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var currentServerItem = platformServerItemBindingSource.Current as PlatformServerItem;
            if (currentServerItem != null)
            {
                var server = _ctx.GetServerDetailsByID(currentServerItem.ID);
                var serverPlarformInfo = _ctx.GetServerPlatformInfoByServer(server);
                serverPlarformInfoBindingSource.DataSource = serverPlarformInfo;
                serverPlarformInfoBindingSource.ResetBindings(true);

                ServicePartsInfo.Server = server;
                UpdateServerPartsInfo();
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных хранилищ данных
        /// </summary>
        private void platfromSANItemBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var currentSANItem = platfromSANItemBindingSource.Current as PlatfromSANItem;
            if (currentSANItem != null)
            {
                var san = _ctx.GetSANDetailsByID(currentSANItem.ID);
                var sanPlatformInfo = _ctx.GetSanPlatformInfoBySAN(san);
                sANPlatformInfoBindingSource.DataSource = sanPlatformInfo;
                sANPlatformInfoBindingSource.ResetBindings(true);


                ServicePartsInfo.San = san;
                UpdateServerPartsInfo();
            }
        }

        /// <summary>
        /// Обработчик события изменения значения в numericUpDown,
        /// который отвечает за кол-во используемых ядер
        /// </summary>
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double frequency = (double)numericUpDown1.Value * (ServicePartsInfo.CPUFrequency / ServicePartsInfo.CPUCount);
            ServicePartsInfo.Frequency = frequency;
            textBox_Frequency.Text = frequency.ToString("##.00");
            UpdateServerPartsInfo();
        }

        /// <summary>
        /// Обработчик события изменения значения в numericUpDown,
        /// который отвечает за кол-во используемой оперативной памяти
        /// </summary>
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            ServicePartsInfo.RAMVolume = (double)numericUpDown2.Value;
            UpdateServerPartsInfo();
        }

        /// <summary>
        /// Обработчик события изменения значения в numericUpDown,
        /// который отвечает за кол-во используемый объем жесткого диска
        /// </summary>
        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            ServicePartsInfo.StorageVolume = (double)numericUpDown3.Value;
            UpdateServerPartsInfo();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно поиска типа сервиса
        /// </summary>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var findServiceType = new EditServiceTypeForm(true);
            if (findServiceType.ShowDialog() == DialogResult.OK)
            {
                if (findServiceType.Edited)
                    BindServiceTypes();

                serviceTypeBindingSource.Position =
                    ((BindingList<ServiceType>)serviceTypeBindingSource.DataSource).IndexOf(findServiceType.CurrentServiceType);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно редактирования типов сервисов
        /// </summary>
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            var editServiceType = new EditServiceTypeForm();
            if (editServiceType.ShowDialog() == DialogResult.OK)
            {
                var selectedServiceType = serviceTypeBindingSource.Current as ServiceType;
                BindServiceTypes();
                if (selectedServiceType != null)
                {
                    int pos = serviceTypeBindingSource.IndexOf(selectedServiceType);
                    if (pos > -1)
                        serviceTypeBindingSource.Position = pos;
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит отмену выбранного сервиса
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox_ServiceTypes.SelectedIndex = -1;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных типов сервиса
        /// </summary>
        private void serviceTypeBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (comboBox_ServiceTypes.SelectedIndex == -1)
                ((BindingSource)sender).SuspendBinding();
            else
                ((BindingSource)sender).ResumeBinding();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно добавления программного обеспечения
        /// </summary>
        private void picBtn_AddSoftware_Click(object sender, EventArgs e)
        {
            var selectedIDs = SelectedSoftware.Select(s => s.Id).ToList();
            var selectSoftwareForm = new SelectSoftwareForm(selectedIDs);
            if (selectSoftwareForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    SelectedSoftware.Add(selectSoftwareForm.CurrentSoftware);
                    UpdateSelectedSoftware();
                    ServicePartsInfo.SoftwareInfo = SelectedSoftware;
                    UpdateServerPartsInfo();
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при добавлении выбранного программного обеспечения!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление выбранного программного обеспечения
        /// </summary>
        private void picBtn_DeleteSoftware_Click(object sender, EventArgs e)
        {
            var selectedSoftware = softwareInfoBindingSource.Current as SoftwareInfo;
            if (selectedSoftware == null)
            {
                MessageBox.Show("Программное обеспечение не выбрано!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SelectedSoftware.Remove(selectedSoftware);
            UpdateSelectedSoftware();
            UpdateServerPartsInfo();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который выполняет сохранение изменений
        /// </summary>
        private async void btn_Save_Click(object sender, EventArgs e)
        {
            var currentSANItem = platfromSANItemBindingSource.Current as PlatfromSANItem;
            var currentServerItem = platformServerItemBindingSource.Current as PlatformServerItem;
            var currentPaasType = paasTypeBindingSource.Current as PaasType;

            if (currentPaasType == null || comboBox_PaasTypes.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите платформу для сохранения изменений!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (comboBox_ServiceTypes.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите тип сервиса для сохранения изменений!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (currentServerItem == null)
            {
                MessageBox.Show("Выберите сервер для сохранения изменений!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (currentSANItem == null)
            {
                MessageBox.Show("Выберите хранилище данных для сохранения изменений!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (CurrentService.HDDVolume <= 0 || CurrentService.CoreCount <= 0 || CurrentService.RamCount <= 0)
            {
                MessageBox.Show("Введите корректные значения характеристик сервиса!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (SelectedSoftware.Count == 0)
            {
                MessageBox.Show("Выберите программное обеспечение!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (ServicePartsInfo.CPUCostByHour <= 0 || ServicePartsInfo.RAMCostByHour <= 0 ||
                ServicePartsInfo.SanStorageCostByHour <= 0 || ServicePartsInfo.FinalPrice <= 0)
            {
                MessageBox.Show("Невозможно сохранить изменения, так как произошла ошибка при рассчете стоимости!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                CurrentService.Software.Clear();
                foreach (var softwareInfo in SelectedSoftware)
                    CurrentService.Software.Add(await _ctx.GetSoftwareById(softwareInfo.Id));

                switch (_formMode)
                {
                    case FormMode.Add:
                        await _ctx.AddNewService(CurrentService);
                        MessageBox.Show("Новый сервис успешно создан!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case FormMode.Edit:
                        await _ctx.EditService(CurrentService);
                        MessageBox.Show("Изменения в сервисе успешно сохранены!", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }

                _formMode = FormMode.None;
                Close();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить изменения!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену добавления/редактирования, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditServiceForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_formMode != FormMode.None)
            {
                var result = MessageBox.Show("Изменения не будут сохранены! Продолжить?", "Отмена изменений",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            Owner?.Show();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление выбранного типа платформы
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox_PaasTypes.SelectedIndex = -1;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных платформ сервера
        /// </summary>
        private void serverPlarformInfoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var s = serverPlarformInfoBindingSource.Current as ServerPlarformInfo;
            if (s != null)
            {
                numericUpDown1.Maximum = s.AvalibleCoreCount;
                numericUpDown2.Maximum = (decimal)s.AvalibleRAMVolume;
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных хранилищ данных
        /// </summary>
        private void sANPlatformInfoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var s = sANPlatformInfoBindingSource.Current as SANPlatformInfo;
            if (s != null)
                numericUpDown3.Maximum = (decimal)s.AvalibleVolume;
        }

        /// <summary>
        /// Обработчик события нажатий клавиш клавиатуры на форме
        /// </summary>
        private void EditServiceForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Enter:
                    btn_Save_Click(null, EventArgs.Empty);
                    break;
            }
        }
    }
}
