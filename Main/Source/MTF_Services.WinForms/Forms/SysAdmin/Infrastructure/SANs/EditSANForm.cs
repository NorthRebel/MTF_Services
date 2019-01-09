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
using MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Dictionary;
using MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Parts;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.SANs
{
    /// <summary>
    /// Класс формы редактирования хранилища данных
    /// </summary>
    public partial class EditSANForm : Form
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
        /// Редактируемое хранилище данных.
        /// </summary>
        public SAN CurrentSAN { get; set; }

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновке в источнике данных производителей
        /// </summary>
        private bool _afterManufacturerSuspend;

        /// <summary>
        /// Список поддерживаемых интерфейсов
        /// </summary>
        public BindingList<AvalibleInterface> AvalibleInterfaces { get; set; }

        /// <summary>
        /// Установленные накопители.
        /// </summary>
        public BindingList<InstalledStorageOnSAN> InstalledStorageOnSAN { get; set; }

        /// <summary>
        /// Конструктор формы создания нового хранилища данных
        /// </summary>
        public EditSANForm()
        {
            InitializeComponent();
            UpdateGridsFont(dg_InstalledStorage);

            _ctx = new Context();

            CurrentSAN = new SAN();
            sANBindingSource.DataSource = CurrentSAN;

            BindSANPartsInfo();
            BindInstalledStorageOnSAN();
            BindManufacturer();
            BindStorageInterfaces();
            BindAvalibleInterfacesNew();
            BindStorages();

            btn_Save.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));

            _formMode = FormMode.Add;
            Text = "Cоздание нового хранилища данных";
            comboBox1.SelectedIndex = -1;
        }

        /// <summary>
        /// Конструктор формы редактирования выбранного хранилища данных.
        /// </summary>
        /// <param name="selectedSAN">Выбранное хранилище данных</param>
        public EditSANForm(SAN selectedSAN) : this()
        {
            _formMode = FormMode.Edit;
            Text = "Редактирование хранилища данных";
            CurrentSAN = selectedSAN;
            sANBindingSource.DataSource = CurrentSAN;
            sANBindingSource.ResetBindings(true);
            PrepareBindingsToEditSAN();
        }

        /// <summary>
        /// Инициализация привязок для редактирования выбранного хранилища данных
        /// </summary>
        private void PrepareBindingsToEditSAN()
        {
            BindAvalibleInterfacesForEdit();
            BindStorages();

            manufacturerBindingSource.Position =
                ((BindingList<Manufacturer>)manufacturerBindingSource.DataSource).IndexOf(CurrentSAN.Manufacturer);
            comboBox1.SelectedIndex = manufacturerBindingSource.Position;

            foreach (var s in CurrentSAN.SAN_Storage)
            {
                if (s.Count != null)
                    InstalledStorageOnSAN.Add(new InstalledStorageOnSAN
                    {
                        Manufacturer = s.Strorage.Manufacturer.Name,
                        Model = s.Strorage.Model,
                        Interface = s.Strorage.StrorageInterface.Name,
                        Volume = s.Strorage.Volume,
                        Price = s.Strorage.Price,
                        Count = (byte)s.Count
                    });
            }

            var cfgPartInfo = sANPartsInfoBindingSource.DataSource as SANPartsInfo;
            if (cfgPartInfo != null)
            {
                cfgPartInfo.Maintenance = CurrentSAN.AnnualMaintenance ?? 0;
                var storageSumPrice = InstalledStorageOnSAN.Sum(iss => iss.Price * iss.Count);
                cfgPartInfo.InitialPrice = CurrentSAN.Price - storageSumPrice;
                cfgPartInfo.StorageSumPrice += storageSumPrice;
                cfgPartInfo.UpdateTotalPrice();
                BindSANPartsInfo();
            }
        }

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

        #region Bindings

        /// <summary>
        /// Привязка списка производителей
        /// </summary>
        private void BindManufacturer()
        {
            manufacturerBindingSource.DataSource = _ctx.GetManufacturers();
            comboBox1.DataSource = manufacturerBindingSource;
        }

        /// <summary>
        /// Привязка списка доступных интерфейсов накопителей для нового хранилища данных
        /// </summary>
        private void BindAvalibleInterfacesNew()
        {
            AvalibleInterfaces = new BindingList<AvalibleInterface>();
            avalibleInterfaceBindingSource.DataSource = AvalibleInterfaces;
            dg_AvalibleInterfaces.DataSource = avalibleInterfaceBindingSource;
        }

        /// <summary>
        /// Привязка списка доступных интерфейсов накопителей для редактируемого хранилища данных
        /// </summary>
        private void BindAvalibleInterfacesForEdit()
        {
            AvalibleInterfaces = _ctx.GetAvalibleInterfacesOfSANBS(CurrentSAN.SAN_StorageInt);
            avalibleInterfaceBindingSource.DataSource = AvalibleInterfaces;
            dg_AvalibleInterfaces.DataSource = avalibleInterfaceBindingSource;
        }

        /// <summary>
        /// Привязка списка существующих интерфейсов
        /// </summary>
        private void BindStorageInterfaces()
        {
            strorageInterfaceBindingSource.DataSource = _ctx.GetStorageInterfaces();
            lst_Interfaces.DataSource = strorageInterfaceBindingSource;
        }

        /// <summary>
        /// Привязка списка установленных накопителей на хранилище данных
        /// </summary>
        private void BindInstalledStorageOnSAN()
        {
            InstalledStorageOnSAN = new BindingList<InstalledStorageOnSAN>();
            installedStorageOnSANBindingSource.DataSource = InstalledStorageOnSAN;
            dg_InstalledStorage.DataSource = installedStorageOnSANBindingSource;
        }

        /// <summary>
        /// Привязка объекта, который отображает цены компонентов
        /// </summary>
        private void BindSANPartsInfo()
        {
            var sanPartInfo = sANPartsInfoBindingSource.DataSource as SANPartsInfo;
            sANPartsInfoBindingSource.DataSource = sanPartInfo ?? new SANPartsInfo();
            sANPartsInfoBindingSource.ResetBindings(true);
        }

        /// <summary>
        /// Привязка списка доступных накопителей
        /// </summary>
        private void BindStorages()
        {
            if (AvalibleInterfaces == null || AvalibleInterfaces.Count == 0)
            {
                InstalledStorageOnSAN.Clear();
                ManageStorageControls(SANPartSelectionMode.None);
                var sanPartInfo = sANPartsInfoBindingSource.DataSource as SANPartsInfo;
                if (sanPartInfo != null)
                {
                    sanPartInfo.SelectedStoragePrice = 0;
                    sanPartInfo.StorageSumPrice = 0;
                    sanPartInfo.UpdateTotalPrice();
                    BindSANPartsInfo();
                }
            }
            else
            {
                var storagesByAvalibleInterfaces = _ctx.GetStoragesByAvalibleInterfaces(AvalibleInterfaces);
                strorageBindingSource.DataSource = storagesByAvalibleInterfaces;
                CheckInstalledStorage(storagesByAvalibleInterfaces);
                ManageStorageControls(strorageBindingSource.Count > 0 ? SANPartSelectionMode.OK : SANPartSelectionMode.UnSupported);
            }
        }

        #endregion

        /// <summary>
        /// Проверка установленных накопителей со списком доступных
        /// </summary>
        /// <param name="strorageList">Список доступных накопителей</param>
        private void CheckInstalledStorage(BindingList<Strorage> strorageList)
        {
            var itemsToRemove = new Stack<InstalledStorageOnSAN>();
            var sanPartInfo = sANPartsInfoBindingSource.DataSource as SANPartsInfo;

            foreach (var storageOnServer in InstalledStorageOnSAN)
            {
                var existed = strorageList.SingleOrDefault(rl =>
                    rl.Manufacturer.Name.Equals(storageOnServer.Manufacturer) && rl.Model.Equals(storageOnServer.Model));
                if (existed == null)
                    itemsToRemove.Push(storageOnServer);
            }

            while (itemsToRemove.Count > 0)
            {
                var itemToRemove = itemsToRemove.Pop();
                sanPartInfo.StorageSumPrice -= (itemToRemove.Price * itemToRemove.Count);
                InstalledStorageOnSAN.Remove(itemToRemove);
            }

            sanPartInfo.UpdateTotalPrice();
            BindSANPartsInfo();
        }

        /// <summary>
        /// Управление элементами управления области накопителей
        /// </summary>
        /// <param name="selMode">Выбранный режим</param>
        private void ManageStorageControls(SANPartSelectionMode selMode)
        {
            foreach (Control control in groupBox3.Controls)
                control.Visible = selMode == SANPartSelectionMode.OK;
            lbl_NoInterfacesToStorage.Visible = selMode == SANPartSelectionMode.None;
            lbl_NoInterfacesToStorage.Dock = selMode == SANPartSelectionMode.None ? DockStyle.Fill : DockStyle.None;
            lbl_NoStorages.Visible = selMode == SANPartSelectionMode.UnSupported;
            lbl_NoStorages.Dock = selMode == SANPartSelectionMode.UnSupported ? DockStyle.Fill : DockStyle.None;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет добавление выбранного интерфейса к доступным
        /// </summary>
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            var selectedInterface = strorageInterfaceBindingSource.Current as StrorageInterface;
            if (selectedInterface == null)
            {
                MessageBox.Show("Выберите интерфейс из списка!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                var existedInt = AvalibleInterfaces.SingleOrDefault(ai => ai.Name.Equals(selectedInterface.Name));
                if (existedInt != null)
                {
                    existedInt.Slot_Count++;
                    avalibleInterfaceBindingSource.DataSource = AvalibleInterfaces;
                    dg_AvalibleInterfaces.DataSource = avalibleInterfaceBindingSource;
                    dg_AvalibleInterfaces.Refresh();
                }
                else
                    AvalibleInterfaces.Add(new AvalibleInterface
                    {
                        Name = selectedInterface.Name,
                        Slot_Count = 1
                    });

                BindStorages();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при добавлении интерфейса!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на текстовую метку, 
        /// который открывает диалоговое окно добавления нового накопителя, если он не выбран
        /// </summary>
        private void lbl_NoStorages_Click(object sender, EventArgs e)
        {
            var storageForm = new EditStorageForm();
            if (storageForm.ShowDialog() == DialogResult.OK)
            {
                BindStorages();
                strorageBindingSource.Position =
                    ((BindingList<Strorage>)strorageBindingSource.DataSource).IndexOf(storageForm.CurrentStrorage);
                comboBox_Storage.SelectedIndex = strorageBindingSource.Position;
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных доступных интерфейсов накопителей
        /// </summary>
        private void strorageInterfaceBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var bs = sender as BindingSource;
            var selStorage = bs.Current as Strorage;
            if (comboBox_Storage.SelectedIndex == -1)
            {
                bs.SuspendBinding();
                selStorage = null;
            }
            else
                bs.ResumeBinding();

            var sanPartInfo = sANPartsInfoBindingSource.DataSource as SANPartsInfo;
            if (sanPartInfo != null)
            {
                sanPartInfo.SelectedStoragePrice = selStorage != null ? selStorage.Price : 0;
                BindSANPartsInfo();
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет удаление выбранного накопителя из установленных
        /// </summary>
        private void pictureBox14_Click(object sender, EventArgs e)
        {
            var selectedStorage = strorageBindingSource.Current as Strorage;
            if (selectedStorage == null)
            {
                MessageBox.Show("Накопитель не выбран!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var storageToDel = InstalledStorageOnSAN.SingleOrDefault(irs =>
                irs.Model.Equals(selectedStorage.Model) && irs.Manufacturer.Equals(selectedStorage.Manufacturer.Name));
            if (storageToDel == null)
            {
                MessageBox.Show("Выбранный накопитель не найден в списке установленных!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Выбранный накопитель будет удален из хранилища данных! Продолжить?",
                "Удаление накопителя", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var sanPartInfo = sANPartsInfoBindingSource.DataSource as SANPartsInfo;
                if (sanPartInfo != null)
                {
                    sanPartInfo.StorageSumPrice -= (storageToDel.Price * storageToDel.Count);
                    sanPartInfo.UpdateTotalPrice();
                    BindSANPartsInfo();
                }
                InstalledStorageOnSAN.Remove(storageToDel);
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
                InstalledStorageOnSAN.Where(iss => iss.Interface.Equals(avalibleInterface.Name)).ToList();
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
                var existedItem = InstalledStorageOnSAN.SingleOrDefault(iss =>
                    iss.Manufacturer.Equals(selectedStorage.Manufacturer.Name) && iss.Model.Equals(selectedStorage.Model));
                if (existedItem != null)
                {
                    existedItem.Count++;
                    installedStorageOnSANBindingSource.DataSource = InstalledStorageOnSAN;
                    dg_InstalledStorage.DataSource = installedStorageOnSANBindingSource;
                    dg_InstalledStorage.Refresh();
                }
                else
                    InstalledStorageOnSAN.Add(new InstalledStorageOnSAN
                    {
                        Model = selectedStorage.Model,
                        Manufacturer = selectedStorage.Manufacturer.Name,
                        Volume = selectedStorage.Volume,
                        Interface = selectedStorage.StrorageInterface.Name,
                        Count = 1,
                        Price = selectedStorage.Price
                    });

                var sanPartInfo = sANPartsInfoBindingSource.DataSource as SANPartsInfo;
                if (sanPartInfo != null)
                {
                    sanPartInfo.StorageSumPrice += selectedStorage.Price;
                    sanPartInfo.UpdateTotalPrice();
                    BindSANPartsInfo();
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при добавлении накопителя!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #region Installed Storage operations

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет увеличение количества выбранного установленного накопителя
        /// </summary>
        private void picBtn_IncreaseStorage_Click(object sender, EventArgs e)
        {
            var selectedInstalledStorage = installedStorageOnSANBindingSource.Current as InstalledStorageOnSAN;
            if (selectedInstalledStorage == null)
            {
                MessageBox.Show("Выберите установленный накопитель из списка!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var avalibleInterface =
                AvalibleInterfaces.Single(av => av.Name.Equals(selectedInstalledStorage.Interface));
            var installedStoragesOfInterface =
                InstalledStorageOnSAN.Where(iss => iss.Interface.Equals(avalibleInterface.Name)).ToList();
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
                installedStorageOnSANBindingSource.DataSource = InstalledStorageOnSAN;
                dg_InstalledStorage.DataSource = InstalledStorageOnSAN;
                dg_InstalledStorage.Refresh();

                var sanPartInfo = sANPartsInfoBindingSource.DataSource as SANPartsInfo;
                if (sanPartInfo != null)
                {
                    sanPartInfo.StorageSumPrice += selectedInstalledStorage.Price;
                    sanPartInfo.UpdateTotalPrice();
                    BindSANPartsInfo();
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
        private void picBtn_DecreaseStorage_Click(object sender, EventArgs e)
        {
            var selectedInstalledStorage = installedStorageOnSANBindingSource.Current as InstalledStorageOnSAN;
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
                    installedStorageOnSANBindingSource.DataSource = InstalledStorageOnSAN;
                    dg_InstalledStorage.DataSource = InstalledStorageOnSAN;
                    dg_InstalledStorage.Refresh();
                    removed = true;
                }
                else
                {
                    var result = MessageBox.Show("Выбранный накопитель будет удален из хранилища данных! Продолжить?",
                        "Удаление накопителя", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        InstalledStorageOnSAN.Remove(selectedInstalledStorage);
                        removed = true;
                    }
                }

                if (removed)
                {
                    var sanPartInfo = sANPartsInfoBindingSource.DataSource as SANPartsInfo;
                    if (sanPartInfo != null)
                    {
                        sanPartInfo.StorageSumPrice -= selectedInstalledStorage.Price;
                        sanPartInfo.UpdateTotalPrice();
                        BindSANPartsInfo();
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
        private void picBtn_DeleteStorage_Click(object sender, EventArgs e)
        {
            var selectedInstalledStorage = installedStorageOnSANBindingSource.Current as InstalledStorageOnSAN;
            if (selectedInstalledStorage == null)
            {
                MessageBox.Show("Выберите установленный накопитель из списка!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Выбранный накопитель будет удален из хранилища данных! Продолжить?",
                "Удаление накопителя", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                var sanPartInfo = sANPartsInfoBindingSource.DataSource as SANPartsInfo;
                if (sanPartInfo != null)
                {
                    sanPartInfo.StorageSumPrice -= (selectedInstalledStorage.Price * selectedInstalledStorage.Count);
                    sanPartInfo.UpdateTotalPrice();
                    BindSANPartsInfo();
                }

                InstalledStorageOnSAN.Remove(selectedInstalledStorage);
            }
        }

        #endregion

        #region Avalible Interfaces operations

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет увеличение количества выбранного установленного интерфейса накопителя
        /// </summary>
        private void pictureBox21_Click(object sender, EventArgs e)
        {
            var selectedInterface = avalibleInterfaceBindingSource.Current as AvalibleInterface;
            if (selectedInterface != null)
            {
                selectedInterface.Slot_Count++;
                avalibleInterfaceBindingSource.DataSource = AvalibleInterfaces;
                dg_AvalibleInterfaces.DataSource = avalibleInterfaceBindingSource;
                dg_AvalibleInterfaces.Refresh();
            }
            else
                MessageBox.Show("Выберите интерфейс из списка или добавьте новый!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет уменьшение количества выбранного установленного интерфейса накопителя
        /// </summary>
        private void pictureBox20_Click(object sender, EventArgs e)
        {
            var selectedInterface = avalibleInterfaceBindingSource.Current as AvalibleInterface;
            if (selectedInterface == null)
            {
                MessageBox.Show("Выберите интерфейс из списка или добавьте новый!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var existedStorages = InstalledStorageOnSAN.Where(iss => iss.Interface.Equals(selectedInterface.Name))
                    .ToList();
                if (existedStorages.Count == 0 || (existedStorages.Sum(es => es.Count) < selectedInterface.Slot_Count))
                {
                    if (selectedInterface.Slot_Count > 1)
                    {
                        selectedInterface.Slot_Count--;
                        avalibleInterfaceBindingSource.DataSource = AvalibleInterfaces;
                        dg_AvalibleInterfaces.DataSource = avalibleInterfaceBindingSource;
                        dg_AvalibleInterfaces.Refresh();
                    }
                    else
                        AvalibleInterfaces.Remove(selectedInterface);
                }
                else
                    MessageBox.Show(
                        "Заняты все слоты выбранного интерфейса! Для удаления интерфейса следует удалить 1 из накопителей!",
                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при удалении интерфейса!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет удаление выбранного установленного интерфейса накопителя
        /// </summary>
        private void pictureBox19_Click(object sender, EventArgs e)
        {
            var selectedInterface = avalibleInterfaceBindingSource.Current as AvalibleInterface;
            if (selectedInterface == null)
            {
                MessageBox.Show("Выберите интерфейс из списка или добавьте новый!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var existedStorages = InstalledStorageOnSAN.Where(iss => iss.Interface.Equals(selectedInterface.Name))
                    .ToList();
                if (existedStorages.Count == 0)
                    AvalibleInterfaces.Remove(selectedInterface);
                else
                {
                    var result = MessageBox.Show(
                        "Вместе с интерфейсом будут удалены накопители, кототрые к нему принадлежат! Продолжить?",
                        "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                    if (result == DialogResult.Yes)
                    {
                        var sanPartInfo = sANPartsInfoBindingSource.DataSource as SANPartsInfo;
                        foreach (var es in existedStorages)
                        {
                            sanPartInfo.StorageSumPrice -= (es.Price * es.Count);
                            InstalledStorageOnSAN.Remove(es);
                        }
                        AvalibleInterfaces.Remove(selectedInterface);
                        sanPartInfo.UpdateTotalPrice();
                        BindSANPartsInfo();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при удалении интерфейса!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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
        private void EditSANForm_FormClosing(object sender, FormClosingEventArgs e)
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

            var manufacturer = manufacturerBindingSource.Current as Manufacturer;
            if (manufacturer == null || comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Выберите производителя для сохранения хранилища данных!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (AvalibleInterfaces.Count == 0)
            {
                MessageBox.Show("Выберите интерфейсы накопителей для сохранения хранилища данных!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (InstalledStorageOnSAN.Count == 0)
            {
                MessageBox.Show("Установите накопители для сохранения хранилища данных!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var sanPartInfo = sANPartsInfoBindingSource.DataSource as SANPartsInfo;
            if (sanPartInfo == null || sanPartInfo.TotalPrice == 0)
            {
                MessageBox.Show("Не удалось получить служебную информацию по текущему хранилищу данных!", "Ошибка",
                    MessageBoxButtons.OK);
                return;
            }

            if (sanPartInfo.Maintenance <= 0)
            {
                MessageBox.Show("Введите стоимость обслуживания хранилища данных!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                CurrentSAN.Manufacturer = manufacturer;
                CurrentSAN.AnnualMaintenance = sanPartInfo.Maintenance;
                CurrentSAN.Price = sanPartInfo.TotalPrice;
                CurrentSAN.SAN_StorageInt.Clear();
                foreach (var ai in AvalibleInterfaces)
                {
                    CurrentSAN.SAN_StorageInt.Add(new SAN_StorageInt
                    {
                        StrorageInterface = _ctx.GetStorageInterfaceByName(ai.Name),
                        SlotCount = ai.Slot_Count
                    });
                }

                CurrentSAN.SAN_Storage.Clear();
                foreach (var storageOnSan in InstalledStorageOnSAN)
                {
                    CurrentSAN.SAN_Storage.Add(new SAN_Storage
                    {
                        Strorage = _ctx.GetStorageByModelManufacturer(storageOnSan.Model, storageOnSan.Manufacturer),
                        Count = storageOnSan.Count
                    });
                }

                switch (_formMode)
                {
                    case FormMode.Add:
                        await _ctx.AddNewSAN(CurrentSAN);
                        MessageBox.Show("Новое хранилище данных успешно создано!", "Информация",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    case FormMode.Edit:
                        await _ctx.EditSAN(CurrentSAN);
                        MessageBox.Show("Изменения в хранилище данных успешно сохранены!", "Информация",
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
                        MessageBox.Show("Не удалось создать новое хранилище данных!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case FormMode.Edit:
                        MessageBox.Show("Не удалось сохранить изменения в хранилище данных!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект,
        /// который осуществляет удаление выбранного производителя
        /// </summary> 
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            manufacturerBindingSource.SuspendBinding();
            if (CurrentSAN != null)
                CurrentSAN.ManufacturerId = 0;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных производителей
        /// </summary>
        private void manufacturerBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (manufacturerBindingSource.IsBindingSuspended)
            {
                _afterManufacturerSuspend = true;
                manufacturerBindingSource.ResumeBinding();
            }
            else if (_afterManufacturerSuspend)
            {
                _afterManufacturerSuspend = false;
                manufacturerBindingSource.Position = comboBox1.SelectedIndex;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно поиска производителя
        /// </summary>
        private void pictureBox17_Click(object sender, EventArgs e)
        {
            var findManufacturer = new EditManufacturerForm(true);
            if (findManufacturer.ShowDialog() == DialogResult.OK)
            {
                if (findManufacturer.Edited)
                {
                    BindManufacturer();
                }

                manufacturerBindingSource_CurrentChanged(manufacturerBindingSource, EventArgs.Empty);
                manufacturerBindingSource.Position =
                    ((BindingList<Manufacturer>)manufacturerBindingSource.DataSource).IndexOf(findManufacturer.CurrentManufacturer);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно редактирования производителей
        /// </summary>
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            var editManufacturer = new EditManufacturerForm();
            if (editManufacturer.ShowDialog() == DialogResult.OK)
            {
                var selectedManufacturer = manufacturerBindingSource.Current as Manufacturer;
                BindManufacturer();
                if (selectedManufacturer != null)
                {
                    int pos = manufacturerBindingSource.IndexOf(selectedManufacturer);
                    if (pos > -1)
                        manufacturerBindingSource.Position = pos;
                }
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего индекса в списке производителей
        /// </summary>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
                manufacturerBindingSource_CurrentChanged(manufacturerBindingSource, EventArgs.Empty);
        }
    }
}
