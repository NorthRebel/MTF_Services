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
using MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Parts;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Servers
{
    /// <summary>
    /// Класс формы редактирования конфигурации сервера.
    /// </summary>
    public partial class EditServerConfigurationForm : Form
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
        /// Редактируемуя конфигурация сервера.
        /// </summary>
        public Server CurrentServer { get; set; }

        /// <summary>
        /// Выбранная платформа
        /// </summary>
        public Platform CurrentPlatform { get; set; }

        /// <summary>
        /// Установленная память.
        /// </summary>
        public BindingList<InstalledRamOnServer> InstalledRamOnServer { get; set; }

        /// <summary>
        /// Установленные накопители.
        /// </summary>
        public BindingList<InstalledStorageOnServer> InstalledStorageOnServer { get; set; }

        /// <summary>
        /// Доступные интерфейсы накопителей.
        /// </summary>
        public BindingList<AvalibleInterface> AvalibleInterfaces { get; set; }

        /// <summary>
        /// Конструктор формы создания новой конфигурации сервера.
        /// </summary>
        public EditServerConfigurationForm()
        {
            InitializeComponent();
            UpdateGridsFont(dg_InstalledRam);
            UpdateGridsFont(dg_InstalledStorage);
            SubscribeMenuItems();

            comboBox_Platform.Format += ComboBox_Platform_Format;
            comboBox_CPU.Format += ComboBox_CPU_Format;
            comboBox_RAM.Format += ComboBox_RAM_Format;
            comboBox_Storage.Format += ComboBox_Storage_Format;

            tsmi_IncreaseRamCount.Click += pictureBox8_Click;
            tsmi_DecreaseRamCount.Click += pictureBox9_Click;
            tsmi_DeleteRam.Click += pictureBox10_Click;

            tsmi_IncreaseStorage.Click += pictureBox21_Click;
            tsmi_DecreaseStorage.Click += pictureBox20_Click;
            tsmi_DeleteStorage.Click += pictureBox19_Click;

            _ctx = new Context();
            CurrentServer = new Server();

            BindConfigurationPartsInfo();
            BindInstalledRamOnServer();
            BindInstalledStorageOnServer();
            BindPlatforms();

            btn_Save.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));

            _formMode = FormMode.Add;
            Text = "Создание новой конфигурации сервера";
        }

        /// <summary>
        /// Конструктор формы редактирования выбранной конфигурации сервера.
        /// </summary>
        /// <param name="selectedServer">Выбранная конфигурация сервера</param>
        public EditServerConfigurationForm(Server selectedServer) : this()
        {
            _formMode = FormMode.Edit;
            Text = "Редактирование конфигурации сервера";
            CurrentServer = selectedServer;
            PrepareBindingsToEditServer();
        }

        /// <summary>
        /// Инициализация привязок для редактирования конфигурации сервера
        /// </summary>
        private void PrepareBindingsToEditServer()
        {

            platformBindingSource.Position =
                ((BindingList<Platform>)platformBindingSource.DataSource).IndexOf(CurrentServer.Platform);
            comboBox_Platform.SelectedIndex = platformBindingSource.Position;
            platformBindingSource_CurrentChanged(platformBindingSource, EventArgs.Empty);

            cPUBindingSource.Position =
                ((BindingList<CPU>)cPUBindingSource.DataSource).IndexOf(CurrentServer.CPU);
            comboBox_CPU.SelectedIndex = cPUBindingSource.Position;
            cPUBindingSource_CurrentChanged(cPUBindingSource, EventArgs.Empty);

            foreach (var s in CurrentServer.Server_Storage)
            {
                InstalledStorageOnServer.Add(new InstalledStorageOnServer
                {
                    Manufacturer = s.Strorage.Manufacturer.Name,
                    Model = s.Strorage.Model,
                    Interface = s.Strorage.StrorageInterface.Name,
                    Volume = s.Strorage.Volume,
                    Price = s.Strorage.Price,
                    Count = s.Count
                });
            }

            foreach (var r in CurrentServer.Server_RAM)
            {
                InstalledRamOnServer.Add(new InstalledRamOnServer
                {
                    Manufacturer = r.RAM.Manufacturer.Name,
                    Model = r.RAM.Model,
                    SingleVolume = r.RAM.Volume,
                    Price = r.RAM.Price,
                    Count = r.Count
                });
            }

            var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
            if (cfgPartInfo != null)
            {
                cfgPartInfo.Maintenance = CurrentServer.AnnualMaintenance ?? 0;
                cfgPartInfo.StorageSumPrice += InstalledStorageOnServer.Sum(iss => iss.Price * iss.Count);
                cfgPartInfo.RAMSumPrice += InstalledRamOnServer.Sum(irs => irs.Price * irs.Count);
                cfgPartInfo.UpdateTotalPrice();
                BindConfigurationPartsInfo();
            }
        }

        #region Форматы comboBox

        /// <summary>
        /// Обработчик события форматирования отображаемого значения в списке накопителей
        /// </summary>
        private void ComboBox_Storage_Format(object sender, ListControlConvertEventArgs e)
        {
            var storageToFormat = e.ListItem as Strorage;
            if (storageToFormat != null)
                e.Value = $"{storageToFormat.Manufacturer.Name} {storageToFormat.Model}";
        }

        /// <summary>
        /// Обработчик события форматирования отображаемого значения в списке оперативной памяти
        /// </summary>
        private void ComboBox_RAM_Format(object sender, ListControlConvertEventArgs e)
        {
            var ramToFormat = e.ListItem as RAM;
            if (ramToFormat != null)
                e.Value = $"{ramToFormat.Manufacturer.Name} {ramToFormat.Model}";
        }

        /// <summary>
        /// Обработчик события форматирования отображаемого значения в списке процессоров
        /// </summary>
        private void ComboBox_CPU_Format(object sender, ListControlConvertEventArgs e)
        {
            var cpuToFormat = e.ListItem as CPU;
            if (cpuToFormat != null)
                e.Value = $"{cpuToFormat.Manufacturer.Name} {cpuToFormat.Model}";
        }

        /// <summary>
        /// Обработчик события форматирования отображаемого значения в списке платформ
        /// </summary>
        private void ComboBox_Platform_Format(object sender, ListControlConvertEventArgs e)
        {
            var platformToFormat = e.ListItem as Platform;
            if (platformToFormat != null)
                e.Value = $"{platformToFormat.Manufacturer.Name} {platformToFormat.Model}";
        }

        #endregion

        #region UI

        /// <summary>
        /// Обновление шрифтов таблиц
        /// </summary>
        private void UpdateGridsFont(DataGridView dataGrid)
        {
            Font f = new Font("Segoe UI Semilight", 11F, GraphicsUnit.Pixel);

            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                column.DefaultCellStyle.Font = f;
                column.HeaderCell.Style.Font = f;
            }
        }

        /// <summary>
        /// Обработчик события выхода курсора из элемента главного меню,
        /// который очищает область в строке состояния.
        /// </summary>
        private void MenuItem_MouseLeave(object sender, EventArgs e)
        {
            tip_Label.Text = string.Empty;
        }

        /// <summary>
        /// Обработчик события навдения курсора на элемент главного меню,
        /// который выводит подсказку элемента в строку состояния.
        /// </summary>
        private void MenuItem_MouseEnter(object sender, EventArgs e)
        {
            tip_Label.Text = ((ToolStripItem)sender).ToolTipText;
        }

        /// <summary>
        /// Подписка элементов главного меню и панели элементов на события 
        /// наведения и выхода курсора для отображения подсказок в строке состояния. 
        /// </summary>
        private void SubscribeMenuItems()
        {
            SubScribeChildMenuItems(menuBar.Items);

            foreach (ToolStripItem toolBarControl in toolBar.Items)
            {
                toolBarControl.MouseEnter += MenuItem_MouseEnter;
                toolBarControl.MouseLeave += MenuItem_MouseLeave;
            }
        }

        /// <summary>
        /// Подписка дочерних элементов главного меню на события 
        /// наведения и выхода курсора для отображения подсказок в строке состояния. 
        /// </summary>
        /// <param name="col">Коллекция элементов главного меню</param>
        private void SubScribeChildMenuItems(ToolStripItemCollection col)
        {
            for (int i = 0; i < col.Count; i++)
            {
                var item = col[i] as ToolStripMenuItem;
                if (item != null)
                {
                    item.MouseEnter += MenuItem_MouseEnter;
                    item.MouseLeave += MenuItem_MouseLeave;
                    if (item.DropDownItems.Count > 0)
                        SubScribeChildMenuItems(item.DropDownItems);
                }
            }
        }

        #endregion

        #region Manage UI controls

        /// <summary>
        /// Управление элементами управления области платформы
        /// </summary>
        /// <param name="selMode">Выбранный режим</param>
        private void ManagePlatformControls(ServerPartSelectionMode selMode)
        {
            foreach (Control control in groupBox1.Controls)
                control.Visible = selMode == ServerPartSelectionMode.OK;
            lbl_NoPlatforms.Visible = selMode == ServerPartSelectionMode.None;
            lbl_NoPlatforms.Dock = selMode == ServerPartSelectionMode.None ? DockStyle.Fill : DockStyle.None;
            lbl_SelectPlatform.Visible = selMode == ServerPartSelectionMode.NotSelected;
            lbl_SelectPlatform.Dock = selMode == ServerPartSelectionMode.NotSelected ? DockStyle.Fill : DockStyle.None;
        }

        /// <summary>
        /// Управление элементами управления области процессоров
        /// </summary>
        /// <param name="selMode">Выбранный режим</param>
        private void ManageCpuControls(ServerPartSelectionMode selMode)
        {
            foreach (Control control in groupBox2.Controls)
                control.Visible = selMode == ServerPartSelectionMode.OK;
            lbl_NoCPUs.Visible = selMode == ServerPartSelectionMode.UnSupported;
            lbl_NoCPUs.Dock = selMode == ServerPartSelectionMode.UnSupported ? DockStyle.Fill : DockStyle.None;
            lbl_NoPlatfromToCPU.Visible = selMode == ServerPartSelectionMode.NotSelected;
            lbl_NoPlatfromToCPU.Dock = selMode == ServerPartSelectionMode.NotSelected ? DockStyle.Fill : DockStyle.None;
            lbl_SelectAvalibleCPU.Visible = selMode == ServerPartSelectionMode.CPUNotSelected;
            lbl_SelectAvalibleCPU.Dock = selMode == ServerPartSelectionMode.CPUNotSelected ? DockStyle.Fill : DockStyle.None;
        }

        /// <summary>
        /// Управление элементами управления области оперативной памяти
        /// </summary>
        /// <param name="selMode">Выбранный режим</param>
        private void ManageRamControls(ServerPartSelectionMode selMode)
        {
            foreach (Control control in groupBox3.Controls)
                control.Visible = selMode == ServerPartSelectionMode.OK;
            lbl_NoRams.Visible = selMode == ServerPartSelectionMode.UnSupported;
            lbl_NoRams.Dock = selMode == ServerPartSelectionMode.UnSupported ? DockStyle.Fill : DockStyle.None;
            lbl_NoPlatfromToRAM.Visible = selMode == ServerPartSelectionMode.NotSelected;
            lbl_NoPlatfromToRAM.Dock = selMode == ServerPartSelectionMode.NotSelected ? DockStyle.Fill : DockStyle.None;
            lbl_SelectAvalibleRAM.Visible = selMode == ServerPartSelectionMode.RAMNotSelected;
            lbl_SelectAvalibleRAM.Dock = selMode == ServerPartSelectionMode.RAMNotSelected ? DockStyle.Fill : DockStyle.None;
        }

        /// <summary>
        /// Управление элементами управления области накопителей
        /// </summary>
        /// <param name="selMode">Выбранный режим</param>
        private void ManageStorageControls(ServerPartSelectionMode selMode)
        {
            foreach (Control control in groupBox5.Controls)
                control.Visible = selMode == ServerPartSelectionMode.OK;
            lbl_NoStorages.Visible = selMode == ServerPartSelectionMode.UnSupported;
            lbl_NoStorages.Dock = selMode == ServerPartSelectionMode.UnSupported ? DockStyle.Fill : DockStyle.None;
            lbl_NoPlatfromToStorage.Visible = selMode == ServerPartSelectionMode.NotSelected;
            lbl_NoPlatfromToStorage.Dock = selMode == ServerPartSelectionMode.NotSelected ? DockStyle.Fill : DockStyle.None;
            lbl_SelectAvalibleStorages.Visible = selMode == ServerPartSelectionMode.StorageNotSelected;
            lbl_SelectAvalibleStorages.Dock = selMode == ServerPartSelectionMode.StorageNotSelected ? DockStyle.Fill : DockStyle.None;
        }

        #endregion

        #region Bindings     

        /// <summary>
        /// Инициализация привязки платформ
        /// </summary>
        private void BindPlatforms()
        {
            platformBindingSource.DataSource = _ctx.GetPlatformsList();
            comboBox_Platform.DataSource = platformBindingSource;
        }

        /// <summary>
        /// Инициализация привязки списка установленной памяти
        /// </summary>
        private void BindInstalledRamOnServer()
        {
            InstalledRamOnServer = new BindingList<InstalledRamOnServer>();
            installedRamOnServerBindingSource.DataSource = InstalledRamOnServer;
            dg_InstalledRam.DataSource = installedRamOnServerBindingSource;
        }

        /// <summary>
        /// Инициализация привязки списка установленных накопителей
        /// </summary>
        private void BindInstalledStorageOnServer()
        {
            InstalledStorageOnServer = new BindingList<InstalledStorageOnServer>();
            installedStorageOnServerBindingSource.DataSource = InstalledStorageOnServer;
            dg_InstalledStorage.DataSource = installedStorageOnServerBindingSource;
        }

        /// <summary>
        /// Получение списка доступных процессоров выбранной платформы
        /// </summary>
        private void BindCPUs()
        {
            if (CurrentPlatform != null)
            {
                cPUBindingSource.DataSource = _ctx.GetCpusOfSocket(CurrentPlatform.CpuSocket.Name);
                if (cPUBindingSource.Count == 0)
                    ManageCpuControls(ServerPartSelectionMode.UnSupported);
            }
            else
            {
                cPUBindingSource.DataSource = null;
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.OneCPUPrice = 0;
                    cfgPartInfo.CPUSumPrice = 0;
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }

                ManageCpuControls(ServerPartSelectionMode.NotSelected);
            }

            comboBox_CPU.DataSource = cPUBindingSource;
        }

        /// <summary>
        /// Получение списка доступной оперативной памяти выбранной платформы
        /// </summary>
        private void BindRAMs()
        {
            if (CurrentPlatform != null)
            {
                var ramsOfRamType = _ctx.GetRamsOfRamType(CurrentPlatform.RamType.Name);
                rAMBindingSource.DataSource = ramsOfRamType;
                CheckInstalledRam(ramsOfRamType);
                if (ramsOfRamType.Count == 0)
                    ManageRamControls(ServerPartSelectionMode.UnSupported);
            }
            else
            {
                rAMBindingSource.DataSource = null;
                InstalledRamOnServer.Clear();
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.SelectedRamPrice = 0;
                    cfgPartInfo.RAMSumPrice = 0;
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }
                ManageRamControls(ServerPartSelectionMode.NotSelected);
            }
            comboBox_RAM.DataSource = rAMBindingSource;
        }

        /// <summary>
        /// Получение списка доступных накопителей выбранной платформы
        /// </summary>
        private void BindStorages()
        {
            if (CurrentPlatform != null)
            {
                var storagesByAvalibleInterfaces = _ctx.GetStoragesByAvaliblePlatformInterfaces(CurrentPlatform.Platform_StorageInt);
                strorageBindingSource.DataSource = storagesByAvalibleInterfaces;
                CheckInstalledStorage(storagesByAvalibleInterfaces);
                if (storagesByAvalibleInterfaces.Count == 0)
                    ManageStorageControls(ServerPartSelectionMode.UnSupported);
            }
            else
            {
                strorageBindingSource.DataSource = null;
                InstalledStorageOnServer.Clear();
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.SelectedStoragePrice = 0;
                    cfgPartInfo.StorageSumPrice = 0;
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }
                ManageStorageControls(ServerPartSelectionMode.NotSelected);
            }

            comboBox_Storage.DataSource = strorageBindingSource;
        }

        /// <summary>
        /// Получение списка доступных интерфейсов накопителей выбранной платформы
        /// </summary>
        private void BindAvalibleInterfaces()
        {
            if (CurrentPlatform != null)
            {
                AvalibleInterfaces = _ctx.GetAvalibleInterfacesOfPlarformBS(CurrentPlatform.Platform_StorageInt);
                avalibleInterfaceBindingSource.DataSource = AvalibleInterfaces;
            }
            else
                avalibleInterfaceBindingSource.DataSource = null;
            dg_AvalibleInterfaces.DataSource = avalibleInterfaceBindingSource;
        }

        /// <summary>
        /// Привязка объекта, который отображает цены компонентов
        /// </summary>
        private void BindConfigurationPartsInfo()
        {
            var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
            configurationPartsInfoBindingSource.DataSource = cfgPartInfo ?? new ConfigurationPartsInfo();
            configurationPartsInfoBindingSource.ResetBindings(true);
        }

        /// <summary>
        /// Получение подробного описания платформы
        /// </summary>
        private void BindPlatfromInfo()
        {
            if (CurrentPlatform != null)
                platformInfoBindingSource.DataSource = _ctx.GetPlatformInfoByPlatform(CurrentPlatform);
        }

        #endregion

        /// <summary>
        /// Проверка установленной памяти в конфигурации с доступным списком ОЗУ
        /// </summary>
        /// <param name="ramList">Список доступных ОЗУ</param>
        private void CheckInstalledRam(BindingList<RAM> ramList)
        {
            var itemsToRemove = new Stack<InstalledRamOnServer>();

            foreach (var ramOnServer in InstalledRamOnServer)
            {
                var existed = ramList.SingleOrDefault(rl =>
                    rl.Manufacturer.Name.Equals(ramOnServer.Manufacturer) && rl.Model.Equals(ramOnServer.Model));
                if (existed == null)
                    itemsToRemove.Push(ramOnServer);
            }

            while (itemsToRemove.Count > 0)
            {
                var itemToRemove = itemsToRemove.Pop();
                InstalledRamOnServer.Remove(itemToRemove);
            }
            var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
            if (cfgPartInfo != null)
            {
                cfgPartInfo.RAMSumPrice = 0;
                cfgPartInfo.UpdateTotalPrice();
                BindConfigurationPartsInfo();
            }
        }

        /// <summary>
        /// Проверка установленных накопителей со списком доступных
        /// </summary>
        /// <param name="strorageList">Список доступных накопителей</param>
        private void CheckInstalledStorage(BindingList<Strorage> strorageList)
        {
            var itemsToRemove = new Stack<InstalledStorageOnServer>();
            var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;

            foreach (var storageOnServer in InstalledStorageOnServer)
            {
                var existed = strorageList.SingleOrDefault(rl =>
                    rl.Manufacturer.Name.Equals(storageOnServer.Manufacturer) && rl.Model.Equals(storageOnServer.Model));
                if (existed == null)
                    itemsToRemove.Push(storageOnServer);
            }

            while (itemsToRemove.Count > 0)
            {
                var itemToRemove = itemsToRemove.Pop();
                cfgPartInfo.StorageSumPrice -= (itemToRemove.Price * itemToRemove.Count);
                InstalledStorageOnServer.Remove(itemToRemove);
            }

            cfgPartInfo.UpdateTotalPrice();
            BindConfigurationPartsInfo();
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных платформ
        /// </summary>
        private void platformBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var bs = sender as BindingSource;
            CurrentPlatform = bs.Current as Platform;
            if (comboBox_Platform.SelectedIndex == -1)
            {
                bs.SuspendBinding();
                CurrentPlatform = null;
            }
            else
                bs.ResumeBinding();

            BindAvalibleInterfaces();

            if (CurrentPlatform != null)
            {
                try
                {
                    CurrentPlatform = _ctx.GetPlatformInclude(CurrentPlatform);

                    ManagePlatformControls(ServerPartSelectionMode.OK);
                    CurrentServer.Platform = CurrentPlatform;

                    var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                    if (cfgPartInfo != null)
                    {
                        cfgPartInfo.PlatfromPrice = CurrentPlatform.Price;
                        cfgPartInfo.UpdateTotalPrice();
                        BindConfigurationPartsInfo();
                    }

                    BindPlatfromInfo();
                    BindCPUs();
                    BindRAMs();
                    BindStorages();
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при получении доступных компонентов для выбранной платформы!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.PlatfromPrice = 0;
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }

                if (bs.Count < 0)
                    ManagePlatformControls(ServerPartSelectionMode.None);
                else
                    ManagePlatformControls(ServerPartSelectionMode.NotSelected);

                BindCPUs();
                BindRAMs();
                BindStorages();
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных процессоров
        /// </summary>
        private void cPUBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            bool unSelected = false;
            var bs = sender as BindingSource;
            var selCPU = bs.Current as CPU;
            if (comboBox_CPU.SelectedIndex == -1)
            {
                bs.SuspendBinding();
                selCPU = null;
                unSelected = true;
            }
            else
                bs.ResumeBinding();

            if (selCPU != null)
            {
                ManageCpuControls(ServerPartSelectionMode.OK);
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                var plarformInfo = platformInfoBindingSource.DataSource as PlatformInfo;
                if (cfgPartInfo != null && plarformInfo != null)
                {
                    cfgPartInfo.OneCPUPrice = selCPU.Price;
                    cfgPartInfo.CPUSumPrice = plarformInfo.CPU_Count * selCPU.Price;
                }
                else if (cfgPartInfo != null)
                {
                    cfgPartInfo.OneCPUPrice = 0;
                    cfgPartInfo.CPUSumPrice = 0;
                }

                if (cfgPartInfo != null)
                {
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }
            }
            else
            {
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.OneCPUPrice = 0;
                    cfgPartInfo.CPUSumPrice = 0;
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }

                if (!unSelected)
                    ManageCpuControls(ServerPartSelectionMode.UnSupported);
                else
                    ManageCpuControls(ServerPartSelectionMode.CPUNotSelected);
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных оперативной памяти
        /// </summary>
        private void rAMBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            bool unSelected = false;
            var bs = sender as BindingSource;
            var selRAM = bs.Current as RAM;
            if (comboBox_RAM.SelectedIndex == -1)
            {
                bs.SuspendBinding();
                selRAM = null;
                unSelected = true;
            }
            else
                bs.ResumeBinding();

            if (selRAM != null)
            {
                ManageRamControls(ServerPartSelectionMode.OK);
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.SelectedRamPrice = selRAM.Price;
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }
            }
            else
            {
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.SelectedRamPrice = 0;
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }

                if (!unSelected)
                    ManageRamControls(ServerPartSelectionMode.UnSupported);
                else
                    ManageRamControls(ServerPartSelectionMode.RAMNotSelected);
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных накопителей
        /// </summary>
        private void strorageBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            bool unSelected = false;
            var bs = sender as BindingSource;
            var selStrorage = bs.Current as Strorage;
            if (comboBox_Storage.SelectedIndex == -1)
            {
                bs.SuspendBinding();
                selStrorage = null;
                unSelected = true;
            }
            else
                bs.ResumeBinding();

            if (selStrorage != null)
            {
                ManageStorageControls(ServerPartSelectionMode.OK);
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.SelectedStoragePrice = selStrorage.Price;
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }
            }
            else
            {
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.SelectedStoragePrice = 0;
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }

                if (!unSelected)
                    ManageStorageControls(ServerPartSelectionMode.UnSupported);
                else
                    ManageStorageControls(ServerPartSelectionMode.RAMNotSelected);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление текущей платформы из конфигурации
        /// </summary>
        private void picBtn_DeletePlatform_Click(object sender, EventArgs e)
        {
            if (CurrentPlatform == null)
            {
                MessageBox.Show("Платформа не выбрана!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var result = MessageBox.Show("Выбранная платформа будет удалена из конфигурации! Продолжить?",
                "Удаление платформы", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                comboBox_Platform.SelectedIndex = -1;
                platformBindingSource_CurrentChanged(platformBindingSource, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно поиска платформы
        /// </summary>
        private void picBtn_SearchPlatform_Click(object sender, EventArgs e)
        {
            var selPlatformForm = new SelectPlatformForm();
            if (selPlatformForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BindPlatforms();
                    Platform selPl = _ctx.GetPlatformByPlatformInfo(selPlatformForm.SelectedPlatformInfo);
                    platformBindingSource.Position =
                        ((BindingList<Platform>)platformBindingSource.DataSource).IndexOf(selPl);
                    comboBox_Platform.SelectedIndex = platformBindingSource.Position;
                    platformBindingSource_CurrentChanged(platformBindingSource, EventArgs.Empty);
                }
                catch
                {
                    MessageBox.Show("Ошибка при выборе платформы!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление текущего процессора из конфигурации
        /// </summary>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var currentCPU = cPUBindingSource.Current as CPU;

            if (currentCPU == null)
            {
                MessageBox.Show("Процессор не выбран!", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var result = MessageBox.Show("Выбранный процессор будет удален из конфигурации! Продолжить?",
                "Удаление платформы", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                comboBox_CPU.SelectedIndex = -1;
                cPUBindingSource_CurrentChanged(cPUBindingSource, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление установленной оперативной памяти
        /// </summary>
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            var selectedRam = rAMBindingSource.Current as RAM;
            if (selectedRam == null)
            {
                MessageBox.Show("Оперативная память не выбрана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var ramToDel = InstalledRamOnServer.SingleOrDefault(irs =>
                irs.Model.Equals(selectedRam.Model) && irs.Manufacturer.Equals(selectedRam.Manufacturer.Name));
            if (ramToDel == null)
            {
                MessageBox.Show("Выбранная оперативная память не найдена в списке установленных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Выбранная оперативная память будет удалена из конфигурации! Продолжить?",
                "Удаление оперативной памяти", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.RAMSumPrice -= (ramToDel.Price * ramToDel.Count);
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }
                InstalledRamOnServer.Remove(ramToDel);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление установленного накопителя
        /// </summary>
        private void pictureBox14_Click(object sender, EventArgs e)
        {
            var selectedStorage = strorageBindingSource.Current as Strorage;
            if (selectedStorage == null)
            {
                MessageBox.Show("Накопитель не выбран!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var storageToDel = InstalledStorageOnServer.SingleOrDefault(irs =>
                irs.Model.Equals(selectedStorage.Model) && irs.Manufacturer.Equals(selectedStorage.Manufacturer.Name));
            if (storageToDel == null)
            {
                MessageBox.Show("Выбранный накопитель не найден в списке установленных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Выбранный накопитель будет удален из конфигурации! Продолжить?",
                "Удаление накопителя", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.StorageSumPrice -= (storageToDel.Price * storageToDel.Count);
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }
                InstalledStorageOnServer.Remove(storageToDel);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно поиска процессора
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var selectCpuForm = new SelectCPUForm(CurrentPlatform.CpuSocket);
            if (selectCpuForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BindCPUs();
                    cPUBindingSource.Position =
                        ((BindingList<CPU>)cPUBindingSource.DataSource).IndexOf(selectCpuForm.SelectedCPU);
                    comboBox_CPU.SelectedIndex = cPUBindingSource.Position;
                    cPUBindingSource_CurrentChanged(cPUBindingSource, EventArgs.Empty);
                }
                catch
                {
                    MessageBox.Show("Ошибка при выборе процессора!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно поиска оперативной памяти
        /// </summary>
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            var selectRamForm = new SelectRAMForm(CurrentPlatform.RamType);
            if (selectRamForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BindRAMs();
                    rAMBindingSource.Position =
                        ((BindingList<RAM>)rAMBindingSource.DataSource).IndexOf(selectRamForm.SelectedRAM);
                    comboBox_RAM.SelectedIndex = rAMBindingSource.Position;
                    rAMBindingSource_CurrentChanged(rAMBindingSource, EventArgs.Empty);
                }
                catch
                {
                    MessageBox.Show("Ошибка при выборе оперативной памяти!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно поиска накопителя
        /// </summary>
        private void pictureBox15_Click(object sender, EventArgs e)
        {
            var selectStorageForm = new SelectStorageForm();
            if (selectStorageForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BindStorages();
                    strorageBindingSource.Position =
                        ((BindingList<Strorage>)strorageBindingSource.DataSource).IndexOf(selectStorageForm.SelectedStorage);
                    comboBox_Storage.SelectedIndex = strorageBindingSource.Position;
                    strorageBindingSource_CurrentChanged(strorageBindingSource, EventArgs.Empty);
                }
                catch
                {
                    MessageBox.Show("Ошибка при выборе накопителя!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего индекса в списке процессоров
        /// </summary>
        private void comboBox_CPU_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > -1)
                ManageCpuControls(ServerPartSelectionMode.OK);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет добавление выбранной памяти к установленной
        /// </summary>
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            var selectedRAM = rAMBindingSource.Current as RAM;
            if (selectedRAM == null)
            {
                MessageBox.Show("Выберите оперативную память из списка!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            int avalibleSlotCount = CurrentPlatform.RamSocketCount - InstalledRamOnServer.Sum(ir => ir.Count);
            if (avalibleSlotCount == 0)
            {
                MessageBox.Show("Все разъемы под оперативную память заняты!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            int avalibleRamVolume =
                CurrentPlatform.RamVolumeMax - InstalledRamOnServer.Sum(ir => ir.SingleVolume * ir.Count);
            if (avalibleRamVolume == 0)
            {
                MessageBox.Show("Занят максимальный объем оперативной памяти!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var existedItem = InstalledRamOnServer.SingleOrDefault(ir =>
                    ir.Manufacturer.Equals(selectedRAM.Manufacturer.Name) && ir.Model.Equals(selectedRAM.Model));
                if (existedItem != null)
                {
                    existedItem.Count++;
                    installedRamOnServerBindingSource.DataSource = InstalledRamOnServer;
                    dg_InstalledRam.DataSource = installedRamOnServerBindingSource;
                    dg_InstalledRam.Refresh();
                }
                else
                    InstalledRamOnServer.Add(new InstalledRamOnServer
                    {
                        Model = selectedRAM.Model,
                        Manufacturer = selectedRAM.Manufacturer.Name,
                        SingleVolume = selectedRAM.Volume,
                        Count = 1,
                        Price = selectedRAM.Price
                    });
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.RAMSumPrice += selectedRAM.Price;
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при добавлении оперативной памяти!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет добавление выбранного накопителя к установленным
        /// </summary>
        private void pictureBox13_Click(object sender, EventArgs e)
        {
            var selectedStorage = strorageBindingSource.Current as Strorage;
            if (selectedStorage == null)
            {
                MessageBox.Show("Выберите накопитель из списка!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var avalibleInterface =
                AvalibleInterfaces.Single(av => av.Name.Equals(selectedStorage.StrorageInterface.Name));
            var installedStoragesOfInterface =
                InstalledStorageOnServer.Where(iss => iss.Interface.Equals(avalibleInterface.Name)).ToList();
            if (installedStoragesOfInterface.Count > 0)
            {
                int avalibleSlotCount =
                    avalibleInterface.Slot_Count - installedStoragesOfInterface.Sum(isi => isi.Count);
                if (avalibleSlotCount == 0)
                {
                    MessageBox.Show($"Все разъемы под интерфейс {avalibleInterface.Name} заняты!", "Предупреждение", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }
            }

            try
            {
                var existedItem = InstalledStorageOnServer.SingleOrDefault(iss =>
                    iss.Manufacturer.Equals(selectedStorage.Manufacturer.Name) && iss.Model.Equals(selectedStorage.Model));
                if (existedItem != null)
                {
                    existedItem.Count++;
                    installedStorageOnServerBindingSource.DataSource = InstalledStorageOnServer;
                    dg_InstalledStorage.DataSource = installedStorageOnServerBindingSource;
                    dg_InstalledStorage.Refresh();
                }
                else
                    InstalledStorageOnServer.Add(new InstalledStorageOnServer
                    {
                        Model = selectedStorage.Model,
                        Manufacturer = selectedStorage.Manufacturer.Name,
                        Volume = selectedStorage.Volume,
                        Interface = selectedStorage.StrorageInterface.Name,
                        Count = 1,
                        Price = selectedStorage.Price
                    });
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.StorageSumPrice += selectedStorage.Price;
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при добавлении накопителя!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на текстовую метку, 
        /// который открывает диалоговое окно добавления нового накопителя, если он не выбран
        /// </summary>
        private void lbl_NoStorages_Click(object sender, EventArgs e)
        {
            var editStorageForm = new EditStorageForm();
            if (editStorageForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BindStorages();
                    strorageBindingSource.Position =
                        ((BindingList<Strorage>)strorageBindingSource.DataSource).IndexOf(editStorageForm.CurrentStrorage);
                    comboBox_Storage.SelectedIndex = strorageBindingSource.Position;
                    strorageBindingSource_CurrentChanged(strorageBindingSource, EventArgs.Empty);
                }
                catch
                {
                    MessageBox.Show("Ошибка при выборе накопителя!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на текстовую метку, 
        /// который открывает диалоговое окно добавления новой оперативной памяти, если она не выбрана
        /// </summary>
        private void lbl_NoRams_Click(object sender, EventArgs e)
        {
            var editRamForm = new EditRAMForm();
            if (editRamForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BindRAMs();
                    rAMBindingSource.Position =
                        ((BindingList<RAM>)rAMBindingSource.DataSource).IndexOf(editRamForm.CurrentRAM);
                    comboBox_RAM.SelectedIndex = rAMBindingSource.Position;
                    rAMBindingSource_CurrentChanged(rAMBindingSource, EventArgs.Empty);
                }
                catch
                {
                    MessageBox.Show("Ошибка при выборе оперативной памяти!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на текстовую метку, 
        /// который открывает диалоговое окно добавления нового процессора, если он не выбран
        /// </summary>
        private void lbl_NoCPUs_Click(object sender, EventArgs e)
        {
            var editCpuForm = new EditCPUForm();
            if (editCpuForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    BindCPUs();
                    cPUBindingSource.Position =
                        ((BindingList<CPU>)cPUBindingSource.DataSource).IndexOf(editCpuForm.CurrentCPU);
                    comboBox_CPU.SelectedIndex = cPUBindingSource.Position;
                    cPUBindingSource_CurrentChanged(cPUBindingSource, EventArgs.Empty);
                }
                catch
                {
                    MessageBox.Show("Ошибка при выборе процессора!",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        #region Installed RAM operations

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет увеличение количества выбранной установленной оперативной памяти
        /// </summary>
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            var selectedInstalledRAM = installedRamOnServerBindingSource.Current as InstalledRamOnServer;
            if (selectedInstalledRAM == null)
            {
                MessageBox.Show("Выберите установленную оперативную память из списка!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            int avalibleSlotCount = CurrentPlatform.RamSocketCount - InstalledRamOnServer.Sum(ir => ir.Count);
            if (avalibleSlotCount == 0)
            {
                MessageBox.Show("Все разъемы под оперативную память заняты!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            int avalibleRamVolume =
                CurrentPlatform.RamVolumeMax - InstalledRamOnServer.Sum(ir => ir.SingleVolume * ir.Count);
            if (avalibleRamVolume == 0)
            {
                MessageBox.Show("Занят максимальный объем оперативной памяти!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                selectedInstalledRAM.Count++;
                installedRamOnServerBindingSource.DataSource = InstalledRamOnServer;
                dg_InstalledRam.DataSource = installedRamOnServerBindingSource;
                dg_InstalledRam.Refresh();

                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.RAMSumPrice += selectedInstalledRAM.Price;
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при добавлении оперативной памяти!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет уменьшение количества выбранной установленной оперативной памяти
        /// </summary>
        private void pictureBox9_Click(object sender, EventArgs e)
        {
            var selectedInstalledRAM = installedRamOnServerBindingSource.Current as InstalledRamOnServer;
            if (selectedInstalledRAM == null)
            {
                MessageBox.Show("Выберите установленную оперативную память из списка!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                bool removed = false;

                if (selectedInstalledRAM.Count > 1)
                {
                    selectedInstalledRAM.Count--;
                    installedRamOnServerBindingSource.DataSource = InstalledRamOnServer;
                    dg_InstalledRam.DataSource = installedRamOnServerBindingSource;
                    dg_InstalledRam.Refresh();
                    removed = true;
                }
                else
                {
                    var result = MessageBox.Show("Выбранная оперативная память будет удалена из конфигурации! Продолжить?",
                        "Удаление оперативной памяти", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        InstalledRamOnServer.Remove(selectedInstalledRAM);
                        removed = true;
                    }
                }

                if (removed)
                {
                    var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                    if (cfgPartInfo != null)
                    {
                        cfgPartInfo.RAMSumPrice -= selectedInstalledRAM.Price;
                        cfgPartInfo.UpdateTotalPrice();
                        BindConfigurationPartsInfo();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при удалении оперативной памяти!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет удаление выбранной установленной оперативной памяти
        /// </summary>
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            var selectedInstalledRAM = installedRamOnServerBindingSource.Current as InstalledRamOnServer;
            if (selectedInstalledRAM == null)
            {
                MessageBox.Show("Выберите установленную оперативную память из списка!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Выбранная оперативная память будет удалена из конфигурации! Продолжить?",
                "Удаление оперативной памяти", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.RAMSumPrice -= (selectedInstalledRAM.Price * selectedInstalledRAM.Count);
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }

                InstalledRamOnServer.Remove(selectedInstalledRAM);
            }
        }

        #endregion

        #region Installed Storage operations

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет увеличение количества выбранного установленного накопителя
        /// </summary>
        private void pictureBox21_Click(object sender, EventArgs e)
        {
            var selectedInstalledStorage = installedStorageOnServerBindingSource.Current as InstalledStorageOnServer;
            if (selectedInstalledStorage == null)
            {
                MessageBox.Show("Выберите установленный накопитель из списка!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var avalibleInterface =
                AvalibleInterfaces.Single(av => av.Name.Equals(selectedInstalledStorage.Interface));
            var installedStoragesOfInterface =
                InstalledStorageOnServer.Where(iss => iss.Interface.Equals(avalibleInterface.Name)).ToList();
            if (installedStoragesOfInterface.Count > 0)
            {
                int avalibleSlotCount =
                    avalibleInterface.Slot_Count - installedStoragesOfInterface.Sum(isi => isi.Count);
                if (avalibleSlotCount == 0)
                {
                    MessageBox.Show($"Все разъемы под интерфейс {avalibleInterface.Name} заняты!", "Предупреждение", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }
            }

            try
            {
                selectedInstalledStorage.Count++;
                installedStorageOnServerBindingSource.DataSource = InstalledStorageOnServer;
                dg_InstalledStorage.DataSource = InstalledStorageOnServer;
                dg_InstalledStorage.Refresh();

                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.StorageSumPrice += selectedInstalledStorage.Price;
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при добавлении накопителя!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет уменьшение количества выбранного установленного накопителя
        /// </summary>
        private void pictureBox20_Click(object sender, EventArgs e)
        {
            var selectedInstalledStorage = installedStorageOnServerBindingSource.Current as InstalledStorageOnServer;
            if (selectedInstalledStorage == null)
            {
                MessageBox.Show("Выберите установленный накопитель из списка!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                bool removed = false;

                if (selectedInstalledStorage.Count > 1)
                {
                    selectedInstalledStorage.Count--;
                    installedStorageOnServerBindingSource.DataSource = InstalledStorageOnServer;
                    dg_InstalledStorage.DataSource = InstalledStorageOnServer;
                    dg_InstalledStorage.Refresh();
                    removed = true;
                }
                else
                {
                    var result = MessageBox.Show("Выбранный накопитель будет удален из конфигурации! Продолжить?",
                        "Удаление накопителя", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        InstalledStorageOnServer.Remove(selectedInstalledStorage);
                        removed = true;
                    }
                }

                if (removed)
                {
                    var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                    if (cfgPartInfo != null)
                    {
                        cfgPartInfo.StorageSumPrice -= selectedInstalledStorage.Price;
                        cfgPartInfo.UpdateTotalPrice();
                        BindConfigurationPartsInfo();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при удалении накопителя!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет удаление выбранного установленного накопителя
        /// </summary>
        private void pictureBox19_Click(object sender, EventArgs e)
        {
            var selectedInstalledStorage = installedStorageOnServerBindingSource.Current as InstalledStorageOnServer;
            if (selectedInstalledStorage == null)
            {
                MessageBox.Show("Выберите установленный накопитель из списка!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Выбранный накопитель будет удален из конфигурации! Продолжить?",
                "Удаление накопителя", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
                if (cfgPartInfo != null)
                {
                    cfgPartInfo.StorageSumPrice -= (selectedInstalledStorage.Price * selectedInstalledStorage.Count);
                    cfgPartInfo.UpdateTotalPrice();
                    BindConfigurationPartsInfo();
                }

                InstalledStorageOnServer.Remove(selectedInstalledStorage);
            }
        }

        #endregion

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену последнего добавления/редактирования, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditServerConfigurationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_formMode != FormMode.None)
            {
                var result = MessageBox.Show("Изменения не будут сохранены! Продолжить?", "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            Owner?.Show();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который выполняет сохранение изменений
        /// </summary>
        private async void btn_Save_Click(object sender, EventArgs e)
        {
            if (_formMode == FormMode.None)
                return;

            if (CurrentPlatform == null)
            {
                MessageBox.Show("Выберите платформу для сохранения конфигурации сервера!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedCPU = cPUBindingSource.Current as CPU;
            if (selectedCPU == null)
            {
                MessageBox.Show("Выберите процессор для сохранения конфигурации сервера!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (InstalledRamOnServer.Count == 0)
            {
                MessageBox.Show("Установите оперативную память для сохранения конфигурации сервера!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (InstalledRamOnServer.Count == 0)
            {
                MessageBox.Show("Установите оперативную память для сохранения конфигурации сервера!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var cfgPartInfo = configurationPartsInfoBindingSource.DataSource as ConfigurationPartsInfo;
            if (cfgPartInfo == null || cfgPartInfo.TotalPrice == 0)
            {
                MessageBox.Show("Не удалось получить служебную информацию по текущей конфигурации сервера!", "Ошибка",
                    MessageBoxButtons.OK);
                return;
            }

            if (cfgPartInfo.Maintenance <= 0)
            {
                MessageBox.Show("Введите стоимость обслуживания сервера!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                CurrentServer.Platform = CurrentPlatform;
                CurrentServer.CPU = selectedCPU;
                CurrentServer.AnnualMaintenance = cfgPartInfo.Maintenance;
                CurrentServer.Price = cfgPartInfo.TotalPrice;
                CurrentServer.Server_RAM.Clear();
                foreach (var ramOnServer in InstalledRamOnServer)
                {
                    CurrentServer.Server_RAM.Add(new Server_RAM
                    {
                        RAM = await _ctx.GetRAMByModelManufacturer(ramOnServer.Model, ramOnServer.Manufacturer),
                        Count = ramOnServer.Count
                    });
                }

                CurrentServer.Server_Storage.Clear();
                foreach (var storageOnServer in InstalledStorageOnServer)
                {
                    CurrentServer.Server_Storage.Add(new Server_Storage
                    {
                        Strorage = _ctx.GetStorageByModelManufacturer(storageOnServer.Model, storageOnServer.Manufacturer),
                        Count = storageOnServer.Count
                    });
                }

                switch (_formMode)
                {
                    case FormMode.Add:
                        await _ctx.AddNewServerConfiguration(CurrentServer);
                        MessageBox.Show("Новая конфигурация сервера успешно создана!", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case FormMode.Edit:
                        await _ctx.EditServerConfiguration(CurrentServer);
                        MessageBox.Show("Изменения в конфигурации сервера успешно сохранены!", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                }

                _formMode = FormMode.None;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                switch (_formMode)
                {
                    case FormMode.Add:
                        MessageBox.Show("Не удалось создать новую конфигурацию сервера!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case FormMode.Edit:
                        MessageBox.Show("Не удалось сохранить изменения в конфигурации сервера!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }
    }
}
