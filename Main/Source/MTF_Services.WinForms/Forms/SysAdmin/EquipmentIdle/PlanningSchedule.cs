using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;

namespace MTF_Services.WinForms.Forms.SysAdmin.EquipmentIdle
{
    /// <summary>
    /// Класс формы планирования расписания обслуживания
    /// </summary>
    public partial class PlanningSchedule : Form
    {
        private readonly SAN _selectedSan;
        private readonly Server _selectedServer;
        private readonly ScheduleEditType _scheduleEditType;

        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Список поизций расписания обслуживания
        /// </summary>
        public BindingList<ScheduleItem> ScheduleItemsMain { get; set; }

        /// <summary>
        /// Отобранный список поизций расписания обслуживания
        /// </summary>
        public BindingList<ScheduleItem> ScheduleItemsToShow { get; set; }

        /// <summary>
        /// Выбранная позиция расписания обслуживания
        /// </summary>
        public ScheduleItem CurrentScheduleItem { get; set; }

        /// <summary>
        /// Режим просмотра списка
        /// </summary>
        private ScheduleItemsShowMode _scheduleItemsShowMode;

        private ScheduleItemsTimeFormat _scheduleItemsTimeFormat;

        /// <summary>
        /// Конструктор формы планирования расписания обслуживания
        /// </summary>
        private PlanningSchedule(ScheduleEditType scheduleEditType)
        {
            _scheduleEditType = scheduleEditType;
            InitializeComponent();
            SubscribeMenuItems();

            _ctx = new Context();

            dg_Schedule.Columns[3].HeaderText = "Длительность (минуты)";
            _scheduleItemsShowMode = ScheduleItemsShowMode.All;
            _scheduleItemsTimeFormat = ScheduleItemsTimeFormat.Minutes;

            switch (_scheduleEditType)
            {
                case ScheduleEditType.Server:
                    Text = "Планирование расписания обслуживания сервера";
                    break;
                case ScheduleEditType.SAN:
                    Text = "Планирование расписания обслуживания хранилища данных";
                    break;
            }
        }

        /// <summary>
        /// Конструктор формы планирования расписания обслуживания для сервера
        /// </summary>
        /// <param name="selectedServer">Выбранная конфигурация сервера</param>
        public PlanningSchedule(Server selectedServer) : this(ScheduleEditType.Server)
        {
            _selectedServer = selectedServer;
            BindCollection();
            UpdateStatBar();
        }

        /// <summary>
        /// Конструктор формы планирования расписания обслуживания для хранилища данных
        /// </summary>
        /// <param name="selectedSan">Выбранное хранилище данных</param>
        public PlanningSchedule(SAN selectedSan) : this(ScheduleEditType.SAN)
        {
            _selectedSan = selectedSan;
            BindCollection();
            UpdateStatBar();
        }

        /// <summary>
        /// Привязка коллекции
        /// </summary>
        private void BindCollection()
        {
            switch (_scheduleEditType)
            {
                case ScheduleEditType.Server:
                    ScheduleItemsMain = _ctx.GetScheduleItemsOfServer(_selectedServer);
                    break;
                case ScheduleEditType.SAN:
                    ScheduleItemsMain = _ctx.GetScheduleItemsOfSAN(_selectedSan);
                    break;
            }

            switch (_scheduleItemsTimeFormat)
            {
                case ScheduleItemsTimeFormat.Hours:
                    часыToolStripMenuItem_Click(null, EventArgs.Empty);
                    break;
                case ScheduleItemsTimeFormat.Minutes:
                    минутыToolStripMenuItem_Click(null, EventArgs.Empty);
                    break;
            }
        }

        /// <summary>
        /// Обновление строки состояния
        /// </summary>
        private void UpdateStatBar()
        {
            lbl_PositionTotalCount.Text = $"Общее кол-во позиций: {ScheduleItemsMain?.Count ?? 0}";
            lbl_PositionSelectedCount.Text = $"Отобрано позиций: {dg_Schedule.Rows.Count}";
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
        /// Обработчик события изменения текущего элемента в источнике данных позиций расписания обслуживания
        /// </summary>
        private void scheduleItemBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as ScheduleItem;
            if (selectedItem != null)
                CurrentScheduleItem = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет переход на форму добавления новой позиции расписания обслуживания
        /// </summary>
        private void новаяПозицияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditScheduleItem editScheduleItem = null;

            switch (_scheduleEditType)
            {
                case ScheduleEditType.Server:
                    editScheduleItem = new EditScheduleItem(_selectedServer);
                    break;
                case ScheduleEditType.SAN:
                    editScheduleItem = new EditScheduleItem(_selectedSan);
                    break;
            }

            if (editScheduleItem.ShowDialog() == DialogResult.OK)
            {
                BindCollection();

                try
                {
                    var ownForm = Owner as EquipmentIdleMenu;
                    ownForm.UpdateServerIdleItems();
                }
                catch 
                {
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет переход на форму редактирования выбранной позиции расписания обслуживания
        /// </summary>
        private async void редактироватьВыбраннуюПозициюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentScheduleItem == null)
            {
                MessageBox.Show("Выберите позицию из списка для ее редактирования!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if (CurrentScheduleItem.BeginDate <= DateTime.Now)
            {
                MessageBox.Show("Редактировать можно только запланированные позиции!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Server_MaintenanceShedule selectedServerMaintenanceShedule = null;
            SAN_MaintenanceShedule selectedSanMaintenanceShedule = null;

            try
            {
                switch (_scheduleEditType)
                {
                    case ScheduleEditType.Server:
                        selectedServerMaintenanceShedule =
                            await _ctx.GetServerMaintenanceSheduleByID(CurrentScheduleItem.Id);
                        break;
                    case ScheduleEditType.SAN:
                        selectedSanMaintenanceShedule =
                            await _ctx.GetSANMaintenanceSheduleByID(CurrentScheduleItem.Id);
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Не удалось получить информацию о выбранной позиции расписания обслуживания", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            EditScheduleItem editScheduleItem = null;

            switch (_scheduleEditType)
            {
                case ScheduleEditType.Server:
                    editScheduleItem = new EditScheduleItem(selectedServerMaintenanceShedule);
                    break;
                case ScheduleEditType.SAN:
                    editScheduleItem = new EditScheduleItem(selectedSanMaintenanceShedule);
                    break;
            }

            if (editScheduleItem.ShowDialog() == DialogResult.OK)
            {
                BindCollection();

                try
                {
                    var ownForm = Owner as EquipmentIdleMenu;
                    ownForm.UpdateServerIdleItems();
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который совершает показ всех позиций расписания обслуживания
        /// </summary>
        private void всеЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            scheduleItemBindingSource.DataSource = ScheduleItemsMain;
            dg_Schedule.DataSource = scheduleItemBindingSource;
            UpdateStatBar();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который совершает отбор запланированных позиций расписания обслуживания
        /// </summary>
        private void запланированныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _scheduleItemsShowMode = ScheduleItemsShowMode.Planned;
            ScheduleItemsToShow = new BindingList<ScheduleItem>(ScheduleItemsMain.Where(sim => sim.BeginDate > DateTime.Now).ToList());
            scheduleItemBindingSource.DataSource = ScheduleItemsToShow;
            dg_Schedule.DataSource = scheduleItemBindingSource;
            UpdateStatBar();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который совершает отбор завершенных позиций расписания обслуживания
        /// </summary>
        private void завершенныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _scheduleItemsShowMode = ScheduleItemsShowMode.Completed;
            ScheduleItemsToShow = new BindingList<ScheduleItem>(ScheduleItemsMain.Where(sim => sim.EndDate < DateTime.Now).ToList());
            scheduleItemBindingSource.DataSource = ScheduleItemsToShow;
            dg_Schedule.DataSource = scheduleItemBindingSource;
            UpdateStatBar();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который меняет формат на минуты длительности позиции обслуживания
        /// </summary>
        private void минутыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var scheduleItems = ScheduleItemsMain.ToList();
                scheduleItems.ForEach(si => si.Duration = (si.EndDate - si.BeginDate).TotalMinutes.ToString());
                ScheduleItemsMain = new BindingList<ScheduleItem>(scheduleItems.ToList());

                switch (_scheduleItemsShowMode)
                {
                    case ScheduleItemsShowMode.All:
                        scheduleItemBindingSource.DataSource = ScheduleItemsMain;
                        dg_Schedule.DataSource = scheduleItemBindingSource;
                        break;
                    case ScheduleItemsShowMode.Planned:
                        запланированныеToolStripMenuItem_Click(null, EventArgs.Empty);
                        break;
                    case ScheduleItemsShowMode.Completed:
                        завершенныеToolStripMenuItem_Click(null, EventArgs.Empty);
                        break;
                }

                dg_Schedule.Columns[3].HeaderText = "Длительность (минуты)";
                минутыToolStripMenuItem.Checked = true;
                toolStripMenuItem5.Checked = true;
                часыToolStripMenuItem.Checked = false;
                toolStripMenuItem6.Checked = false;
                _scheduleItemsTimeFormat = ScheduleItemsTimeFormat.Minutes;
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при переводе формата!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который меняет формат на часы длительности позиции обслуживания
        /// </summary>
        private void часыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var scheduleItems = ScheduleItemsMain.ToList();
                scheduleItems.ForEach(si => si.Duration = TimeSpan.FromHours((si.EndDate - si.BeginDate).TotalHours).ToString(@"hh\:mm"));
                ScheduleItemsMain = new BindingList<ScheduleItem>(scheduleItems.ToList());

                switch (_scheduleItemsShowMode)
                {
                    case ScheduleItemsShowMode.All:
                        scheduleItemBindingSource.DataSource = ScheduleItemsMain;
                        dg_Schedule.DataSource = scheduleItemBindingSource;
                        UpdateStatBar();
                        break;
                    case ScheduleItemsShowMode.Planned:
                        запланированныеToolStripMenuItem_Click(null, EventArgs.Empty);
                        break;
                    case ScheduleItemsShowMode.Completed:
                        завершенныеToolStripMenuItem_Click(null, EventArgs.Empty);
                        break;
                }

                dg_Schedule.Columns[3].HeaderText = "Длительность (часы)";
                минутыToolStripMenuItem.Checked = false;
                toolStripMenuItem5.Checked = false;
                часыToolStripMenuItem.Checked = true;
                toolStripMenuItem6.Checked = true;
                _scheduleItemsTimeFormat = ScheduleItemsTimeFormat.Hours;
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при переводе формата!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }            
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который осуществляет удаление выбранной позиции расписания обслуживания
        /// </summary>
        private async void удалитьВыбраннуюПозициюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentScheduleItem == null)
            {
                MessageBox.Show("Выберите позицию из списка для ее удаленияя!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            if (CurrentScheduleItem.BeginDate <= DateTime.Now)
            {
                MessageBox.Show("Удалять можно только запланированные позиции!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Server_MaintenanceShedule selectedServerMaintenanceShedule = null;
            SAN_MaintenanceShedule selectedSanMaintenanceShedule = null;

            try
            {
                switch (_scheduleEditType)
                {
                    case ScheduleEditType.Server:
                        selectedServerMaintenanceShedule =
                            await _ctx.GetServerMaintenanceSheduleByID(CurrentScheduleItem.Id);
                        break;
                    case ScheduleEditType.SAN:
                        selectedSanMaintenanceShedule =
                            await _ctx.GetSANMaintenanceSheduleByID(CurrentScheduleItem.Id);
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Не удалось получить информацию о выбранной позиции расписания обслуживания", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Выбранная позиция будет удалена! Продолжить?",
                "Удаление позиции расписания обслуживания", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Yes)
            {
                try
                {
                    switch (_scheduleEditType)
                    {
                        case ScheduleEditType.Server:
                            await _ctx.DeleteSelectedServiceScheduleMaintenance(selectedServerMaintenanceShedule);
                            break;
                        case ScheduleEditType.SAN:
                            await _ctx.DeleteSelectedSANScheduleMaintenance(selectedSanMaintenanceShedule);
                            break;
                    }

                    BindCollection();

                    try
                    {
                        var ownForm = Owner as EquipmentIdleMenu;
                        ownForm.UpdateServerIdleItems();
                    }
                    catch
                    {
                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при удалении выбранной позиции расписания обслуживания!",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
