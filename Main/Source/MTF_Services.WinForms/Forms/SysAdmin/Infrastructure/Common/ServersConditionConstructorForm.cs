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
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Common
{
    /// <summary>
    /// Класс формы отбора конфигураций серверов
    /// </summary>
    public partial class ServersConditionConstructorForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновки в источнике данных платформ
        /// </summary>
        private bool _afterPlatformSuspend;

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновки в источнике данных процессоров
        /// </summary>
        private bool _afterCPUSuspend;

        /// <summary>
        /// Выбранная платформа
        /// </summary>
        public Platform SelectedPlatform { get; set; }

        /// <summary>
        /// Выбранный процессор
        /// </summary>
        public CPU SelectedCPU { get; set; }

        /// <summary>
        /// Список конфигураций серверов
        /// </summary>
        public BindingList<ServerInfo> ServersInfoMain { get; set; }

        /// <summary>
        /// Отобранный список конфигураций серверов
        /// </summary>
        public BindingList<ServerInfo> ServersInfoToShow { get; set; }

        /// <summary>
        /// Конструктор формы отбора конфигураций серверов
        /// </summary>
        public ServersConditionConstructorForm(BindingList<ServerInfo> serversInfoMain)
        {
            InitializeComponent();
            btn_Cancel.Image = new Bitmap(Resources.no, 20, 20);
            btn_OK.Image = new Bitmap(Resources.camera_test, 20, 20);

            _ctx = new Context();
            ServersInfoMain = serversInfoMain;
            InitBindings();
        }

        /// <summary>
        /// Инициализация привязок
        /// </summary>
        private void InitBindings()
        {
            platformBindingSource.DataSource = _ctx.GetPlatformsList();
            cPUBindingSource.DataSource = _ctx.GetCPUs();
        }

        /// <summary>
        /// Обработчик события форматирования отображаемого значения в combobox для платформы
        /// </summary>
        private void comboBox1_Format(object sender, ListControlConvertEventArgs e)
        {
            var platformToFormat = e.ListItem as Platform;
            if (platformToFormat != null)
                e.Value = $"{platformToFormat.Manufacturer.Name} {platformToFormat.Model}";
        }

        /// <summary>
        /// Обработчик события форматирования отображаемого значения в combobox для процессора
        /// </summary>
        private void comboBox2_Format(object sender, ListControlConvertEventArgs e)
        {
            var cpuToFormat = e.ListItem as CPU;
            if (cpuToFormat != null)
                e.Value = $"{cpuToFormat.Manufacturer.Name} {cpuToFormat.Model} ({cpuToFormat.CpuSocket.Name})";
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных платформ
        /// </summary>
        private void platformBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (platformBindingSource.IsBindingSuspended)
            {
                _afterPlatformSuspend = true;
                platformBindingSource.ResumeBinding();
            }
            else if (_afterPlatformSuspend)
            {
                _afterPlatformSuspend = false;
                platformBindingSource.Position = comboBox1.SelectedIndex;
            }

            if (platformBindingSource.Position > -1)
            {
                var platform = platformBindingSource.Current as Platform;
                if (platform != null)
                {
                    SelectedPlatform = platform;
                    lbl_SelectedConfigsStatus.Text = $"Отобрано конфигураций: {RunSelection()}";
                }
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных процессоров
        /// </summary>
        private void cPUBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (cPUBindingSource.IsBindingSuspended)
            {
                _afterCPUSuspend = true;
                cPUBindingSource.ResumeBinding();
            }
            else if (_afterCPUSuspend)
            {
                _afterCPUSuspend = false;
                cPUBindingSource.Position = comboBox2.SelectedIndex;
            }

            if (cPUBindingSource.Position > -1)
            {
                var cpu = cPUBindingSource.Current as CPU;
                if (cpu != null)
                {
                    SelectedCPU = cpu;
                    lbl_SelectedConfigsStatus.Text = $"Отобрано конфигураций: {RunSelection()}";
                }
            }
        }

        /// <summary>
        /// Выполнение отбора конфигураций серверов данных по условию
        /// </summary>
        private int RunSelection()
        {
            List<ServerInfo> selectedServers = null;

            try
            {
                if (SelectedPlatform != null && SelectedCPU != null)
                    selectedServers = ServersInfoMain.Where(si =>
                            si.Platform.Equals(SelectedPlatform.Manufacturer.Name + " " + SelectedPlatform.Model) && si.CPU.Equals(SelectedCPU.Manufacturer.Name + " " + SelectedCPU.Model))
                        .ToList();
                else if (SelectedPlatform != null)
                    selectedServers = ServersInfoMain.Where(si => si.Platform.Equals(SelectedPlatform.Manufacturer.Name + " " + SelectedPlatform.Model)).ToList();
                else if (SelectedCPU != null)
                    selectedServers = ServersInfoMain.Where(si => si.CPU.Equals(SelectedCPU.Manufacturer.Name + " " + SelectedCPU.Model)).ToList();
                else
                    selectedServers = ServersInfoMain.ToList();

                ServersInfoToShow = new BindingList<ServerInfo>(selectedServers);
                return ServersInfoToShow.Count;
            }
            catch
            {
                ServersInfoToShow = null;
                return 0;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет отмену выбора платформы
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            SelectedPlatform = null;
            platformBindingSource.SuspendBinding();
            lbl_SelectedConfigsStatus.Text = $"Отобрано конфигураций: {RunSelection()}";
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет отмену выбора процессора
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            SelectedCPU = null;
            cPUBindingSource.SuspendBinding();
            lbl_SelectedConfigsStatus.Text = $"Отобрано конфигураций: {RunSelection()}";
        }

        /// <summary>
        /// Обработчик события изменения текущего индекса в списке платформ
        /// </summary>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
                platformBindingSource_CurrentChanged(platformBindingSource, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик события изменения текущего индекса в списке процессоров
        /// </summary>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > -1)
                cPUBindingSource_CurrentChanged(cPUBindingSource, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену совершенного отбора записей
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет подтверждение совершенного отбора конфигураций серверов
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (ServersInfoToShow != null && ServersInfoToShow.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Не найдены конфигурации серверов по указанному условию!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
