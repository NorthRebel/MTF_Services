using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Common;
using MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.SANs;
using MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Servers;

namespace MTF_Services.WinForms.Forms.SysAdmin.Infrastructure
{
    /// <summary>
    /// Класс формы редактирования вычислительной инфраструктуры.
    /// </summary>
    public partial class EditInfrastructureFrom : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Список конфигураций серверов
        /// </summary>
        public BindingList<ServerInfo> ServersInfoMain { get; set; }

        /// <summary>
        /// Отобранный список конфигураций серверов
        /// </summary>
        public BindingList<ServerInfo> ServersInfoToShow { get; set; }

        /// <summary>
        /// Список конфигураций хранилищ данных
        /// </summary>
        public BindingList<SAN_Info> SansInfoMain { get; set; }

        /// <summary>
        /// Отобранный список конфигураций хранилищ данных
        /// </summary>
        public BindingList<SAN_Info> SansInfoToShow { get; set; }

        /// <summary>
        /// Флаг об выводе отобранного списка конфигураций серверов
        /// </summary>
        private bool _serverSelected;

        /// <summary>
        /// Флаг об выводе отобранного списка хранилищ данных
        /// </summary>
        private bool _sansSelected;

        /// <summary>
        /// Конструктор формы редактирования вычислительной инфраструктуры.
        /// </summary>
        public EditInfrastructureFrom()
        {
            InitializeComponent();
            SubscribeMenuItems();

            lbl_NoServerConfigs.Click += созданиеНовойToolStripMenuItem_Click;
            lbl_NoSANs.Click += созданиеНовогоToolStripMenuItem_Click;

            создатьНовыйToolStripMenuItem.Click += созданиеНовойToolStripMenuItem_Click;
            редактироватьToolStripMenuItem.Click += редактированиеВыбраннойToolStripMenuItem_Click;
            удалитьToolStripMenuItem.Click += удалениеВыбраннойToolStripMenuItem_Click;

            toolStripMenuItem1.Click += созданиеНовогоToolStripMenuItem_Click;
            toolStripMenuItem3.Click += редактированиеВыбранногоToolStripMenuItem_Click;
            toolStripMenuItem4.Click += удалениеВыбранногоToolStripMenuItem_Click;

            _ctx = new Context();
            timer.Enabled = true;
        }

        /// <summary>
        /// Обновление привязок данных.
        /// </summary>
        public async void UpdateBindings()
        {
            try
            {
                sanStateBS.DataSource = await _ctx.GetSanEquipmentState();
                serverStateBS.DataSource = await _ctx.GetServersEquipmentState();
            }
            catch
            {
                sanStateBS.DataSource = new EquipmentState();
                serverStateBS.DataSource = new EquipmentState();
            }

            try
            {
                ServersInfoMain = _ctx.GetServerInfo();
                SansInfoMain = _ctx.GetSanInfo();
                serverInfoBindingSource.DataSource = ServersInfoMain;
                sANInfoBindingSource.DataSource = SansInfoMain;

                dataGridView1.DataSource = serverInfoBindingSource;
                dataGridView2.DataSource = sANInfoBindingSource;
                _serverSelected = false;
                _sansSelected = false;
            }
            catch
            {
            }
            finally
            {
                UpdateLabels();
                UpdateStatBar();
            }
        }

        /// <summary>
        /// Скрытие таблицы с кратким описанием конфигураций серверов
        /// </summary>
        private void HideServerGrid()
        {
            dataGridView1.Visible = false;
            dataGridView1.Dock = DockStyle.None;
            lbl_NoServerConfigs.Visible = true;
            lbl_NoServerConfigs.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// Скрытие таблицы с кратким описанием  конфигураций систем хранения данных
        /// </summary>
        private void HideSanGrid()
        {
            dataGridView2.Visible = false;
            dataGridView2.Dock = DockStyle.None;
            lbl_NoSANs.Visible = true;
            lbl_NoSANs.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// Обновление строки состояния
        /// </summary>
        private void UpdateStatBar()
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    lbl_RecordsTotal.Text = $"Общее количество конфигураций серверов: {ServersInfoMain?.Count ?? 0}";
                    lbl_RecordsSelected.Text = $"Отобрано конфигураций: {dataGridView1.Rows.Count}";
                    break;
                case 1:
                    lbl_RecordsTotal.Text = $"Общее количество конфигураций хранилищ данных: {SansInfoMain?.Count ?? 0}";
                    lbl_RecordsSelected.Text = $"Отобрано конфигураций: {dataGridView2.Rows.Count}";
                    break;
            }
        }

        /// <summary>
        /// Обновление таблиц и меток, которые сообщают об отсутствии информации.
        /// </summary>
        private void UpdateLabels()
        {
            try
            {
                if (serverInfoBindingSource.DataSource == null ||
                    (serverInfoBindingSource.DataSource as BindingList<ServerInfo>).Count == 0)
                    HideServerGrid();
                else
                {
                    dataGridView1.Visible = true;
                    dataGridView1.Dock = DockStyle.Fill;
                    lbl_NoServerConfigs.Visible = false;
                    lbl_NoServerConfigs.Dock = DockStyle.None;
                }
            }
            catch
            {
                HideServerGrid();
            }

            try
            {
                if (sANInfoBindingSource.DataSource == null ||
                    (sANInfoBindingSource.DataSource as BindingList<SAN_Info>).Count == 0)
                    HideSanGrid();
                else
                {
                    dataGridView2.Visible = true;
                    dataGridView2.Dock = DockStyle.Fill;
                    lbl_NoSANs.Visible = false;
                    lbl_NoSANs.Dock = DockStyle.None;
                }
            }
            catch
            {
                HideSanGrid();
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

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditInfrastructureFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            Owner?.Show();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет переход на форму добавления новой конфигурации сервера
        /// </summary>
        private void созданиеНовойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var serverConfigurationForm = new EditServerConfigurationForm { Owner = this };
            serverConfigurationForm.Show();
            Hide();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет переход на форму редактирования выбранной конфигурации сервера
        /// </summary>
        private async void редактированиеВыбраннойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedServer = serverInfoBindingSource.Current as ServerInfo;
            if (selectedServer == null)
            {
                MessageBox.Show("Выберите конфигурацию сервера для редактирования!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var serverToEdit = await _ctx.GetServerByServerInfo(selectedServer);

                var serverConfigurationForm = new EditServerConfigurationForm(serverToEdit) { Owner = this };
                serverConfigurationForm.Show();
                Hide();
            }
            catch
            {
                MessageBox.Show("Не удалось получить информацию по выбранной конфигурации сервера!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который осуществляет удаление выбранной конфигурации сервера
        /// </summary>
        private async void удалениеВыбраннойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedServer = serverInfoBindingSource.Current as ServerInfo;
            if (selectedServer == null)
            {
                MessageBox.Show("Выберите конфигурацию сервера для ее удаления!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var serverToDel = await _ctx.GetServerByServerInfo(selectedServer);

                var result = MessageBox.Show("Выбранная конфигурация сервера будет удалена! Продолжить?",
                    "Удаление конфигурации сервера", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _ctx.DeleteServer(serverToDel);
                        UpdateBindings();
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка при удалении конфигурации сервера!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось получить информацию по выбранной конфигурации сервера!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет переход на форму создания нового хранилища данных
        /// </summary>
        private void созданиеНовогоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editSanForm = new EditSANForm { Owner = this };
            editSanForm.Show();
            Hide();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет переход на форму редактирования выбранного хранилища данных
        /// </summary>
        private async void редактированиеВыбранногоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedSAN = sANInfoBindingSource.Current as SAN_Info;
            if (selectedSAN == null)
            {
                MessageBox.Show("Выберите хранилище данных для редактирования!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var sanToEdit = await _ctx.GetSANBySANInfo(selectedSAN);

                var editSanForm = new EditSANForm(sanToEdit) { Owner = this };
                editSanForm.Show();
                Hide();
            }
            catch
            {
                MessageBox.Show("Не удалось получить информацию по выбранному хранилищу данных!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который осуществляет удаление выбранного хранилища данных
        /// </summary>
        private async void удалениеВыбранногоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedSAN = sANInfoBindingSource.Current as SAN_Info;
            if (selectedSAN == null)
            {
                MessageBox.Show("Выберите хранилище данных для его удаления!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var sanToDel = await _ctx.GetSANBySANInfo(selectedSAN);

                var result = MessageBox.Show("Выбранное хранилище данных будет удалено! Продолжить?",
                    "Удаление конфигурации хранилища данных", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _ctx.DeleteSAN(sanToDel);
                        UpdateBindings();
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка при удалении конфигурации хранилища данных!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось получить информацию по выбранному хранилищу данных!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события активации формы
        /// </summary>
        private void EditInfrastructureFrom_Activated(object sender, EventArgs e)
        {
            UpdateBindings();
        }

        /// <summary>
        /// Обработчик события выполнения такта таймера
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (_ctx == null)
                return;

            try
            {
                _ctx.CheckServerMaintenance();
                _ctx.CheckSANMaintenance();
                UpdateBindings();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Обработчик нажатий клавиш на форме
        /// </summary>
        private void EditInfrastructureFrom_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Tab:
                    tabControl1.TabIndex = tabControl1.TabIndex > 0 ? 0 : 1;
                    break;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно для задания условия поиска конфигурации сервера
        /// </summary>
        private void поискToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ServersInfoMain != null && ServersInfoMain.Count > 0)
            {
                var serversCondition = new ServersConditionConstructorForm(ServersInfoMain);
                if (serversCondition.ShowDialog() == DialogResult.OK)
                {
                    int pos = 0;
                    if (!_serverSelected)
                        pos = ServersInfoMain.IndexOf(serversCondition.ServersInfoToShow.First());
                    else
                        pos = ServersInfoToShow.IndexOf(serversCondition.ServersInfoToShow.First());

                    if (pos > -1)
                        serverInfoBindingSource.Position = pos;
                }
            }
            else
                MessageBox.Show("Отсутсвуют конфигурации серверов для совершения поиска!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно для задания условия отбора конфигураций серверов
        /// </summary>
        private void отборToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ServersInfoMain != null && ServersInfoMain.Count > 0)
            {
                var serversCondition = new ServersConditionConstructorForm(ServersInfoMain);
                if (serversCondition.ShowDialog() == DialogResult.OK)
                {
                    _serverSelected = true;
                    ServersInfoToShow = serversCondition.ServersInfoToShow;
                    serverInfoBindingSource.DataSource = ServersInfoToShow;
                    dataGridView1.DataSource = serverInfoBindingSource;
                    UpdateStatBar();
                }
            }
            else
                MessageBox.Show("Отсутсвуют конфигурации серверов для совершения отбора!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который совершает показ всех конфигураций серверов
        /// </summary>
        private void показатьВсеЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ServersInfoMain != null && ServersInfoMain.Count > 0)
            {
                _serverSelected = false;
                serverInfoBindingSource.DataSource = ServersInfoMain;
                dataGridView1.DataSource = serverInfoBindingSource;
                UpdateStatBar();
            }
            else
                MessageBox.Show("Отсутсвуют конфигурации серверов!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно для задания условия поиска конфигурации хранилища данных
        /// </summary>
        private void поискToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SansInfoMain != null && SansInfoMain.Count > 0)
            {
                var sansCondition = new SAN_ConditionConstructorForm(SansInfoMain);
                if (sansCondition.ShowDialog() == DialogResult.OK)
                {
                    int pos = 0;
                    if (!_sansSelected)
                        pos = SansInfoMain.IndexOf(sansCondition.SansInfoToShow.First());
                    else
                        pos = SansInfoToShow.IndexOf(sansCondition.SansInfoToShow.First());

                    if (pos > -1)
                        sANInfoBindingSource.Position = pos;
                }
            }
            else
                MessageBox.Show("Отсутсвуют конфигурации хранилищ данных для совершения поиска!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно для задания условия отбора конфигураций хранилищ данных
        /// </summary>
        private void отборToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SansInfoMain != null && SansInfoMain.Count > 0)
            {
                var sansCondition = new SAN_ConditionConstructorForm(SansInfoMain);
                if (sansCondition.ShowDialog() == DialogResult.OK)
                {
                    _sansSelected = true;
                    SansInfoToShow = sansCondition.SansInfoToShow;
                    sANInfoBindingSource.DataSource = SansInfoToShow;
                    dataGridView2.DataSource = sANInfoBindingSource;
                    UpdateStatBar();
                }
            }
            else
                MessageBox.Show("Отсутсвуют конфигурации хранилищ данных для совершения отбора!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который совершает показ всех конфигураций хранилищ данных
        /// </summary>
        private void показатьВсеЗаписиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SansInfoMain != null && SansInfoMain.Count > 0)
            {
                _sansSelected = false;
                sANInfoBindingSource.DataSource = SansInfoMain;
                dataGridView2.DataSource = sANInfoBindingSource;
                UpdateStatBar();
            }
            else
                MessageBox.Show("Отсутсвуют конфигурации хранилищ данных!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Обработчик события изменения индекса текущей вкладки
        /// </summary>
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatBar();
        }
    }
}
