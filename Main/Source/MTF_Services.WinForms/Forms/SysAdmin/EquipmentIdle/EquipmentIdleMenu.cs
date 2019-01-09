using System;
using System.Windows.Forms;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;

namespace MTF_Services.WinForms.Forms.SysAdmin.EquipmentIdle
{
    /// <summary>
    /// Класс формы меню простоя оборудования
    /// </summary>
    public partial class EquipmentIdleMenu : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Выбранный сервер
        /// </summary>
        public ServerIdleItem CurrentServerIdleItem { get; set; }

        /// <summary>
        /// Выбранное хранилище данных
        /// </summary>
        public SANIdleItem CurrentSanIdleItem { get; set; }

        /// <summary>
        /// Конструктор формы меню простоя оборудования
        /// </summary>
        public EquipmentIdleMenu()
        {
            InitializeComponent();
            SubscribeMenuItems();

            _ctx = new Context();

            BindCollections();
            timer.Enabled = true;
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
        /// Привязка коллекций
        /// </summary>
        private void BindCollections()
        {
            UpdateServerIdleItems();

            sANIdleItemBindingSource.DataSource = _ctx.GetSanIdleItems();
            dg_Sans.DataSource = sANIdleItemBindingSource;

            serviceIdleItemBindingSource.DataSource = _ctx.GetServiceIdleItems();
            dg_Services.DataSource = serviceIdleItemBindingSource;
        }

        /// <summary>
        /// Обновление коллекции простоя серверов
        /// </summary>
        public void UpdateServerIdleItems()
        {
            serverIdleItemBindingSource.DataSource = _ctx.GetServerIdleItems();
            dg_Servers.DataSource = serverIdleItemBindingSource;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных информации о простое сервера
        /// </summary>
        private void serverIdleItemBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as ServerIdleItem;
            if (selectedItem != null)
                CurrentServerIdleItem = selectedItem;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных информации о простое хранилища данных
        /// </summary>
        private void sANIdleItemBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as SANIdleItem;
            if (selectedItem != null)
                CurrentSanIdleItem = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы планирования расписания обслуживания выбранного сервера
        /// </summary>
        private async void tsmi_ServerPlanningMaintenance_Click(object sender, EventArgs e)
        {
            if (CurrentServerIdleItem == null)
            {
                MessageBox.Show("Выберите сервер из списка для планирования расписания обслуживания!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var server = await _ctx.GetServerByID(CurrentServerIdleItem.ID);
                var planningSchedule = new PlanningSchedule(server) { Owner = this };
                planningSchedule.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при получении информации о выбранной конфигурации сервера!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы регистрации нового простоя сервера
        /// </summary>
        private async void tsmi_ServerRegisterIdle_Click(object sender, EventArgs e)
        {
            if (CurrentServerIdleItem == null)
            {
                MessageBox.Show("Выберите сервер из списка для регистрации нового простоя!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var server = await _ctx.GetServerByID(CurrentServerIdleItem.ID);

                if (_ctx.GetServicesByServer(server).Count == 0)
                {
                    MessageBox.Show("Данный сервер не используется на данный момент! " +
                                    "\nРегистрация простоя невозможна!", 
                                    "Предупреждение",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                var registerNewIdle = new RegisterNewIdle(server);
                if (registerNewIdle.ShowDialog() == DialogResult.OK)
                    BindCollections();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при получении информации о выбранной конфигурации сервера!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы планирования расписания обслуживания выбранного хранилища данных
        /// </summary>
        private async void tsmi_SanPlanningMaintenance_Click(object sender, EventArgs e)
        {
            if (CurrentSanIdleItem == null)
            {
                MessageBox.Show("Выберите хранилище данных из списка для планирования расписания обслуживания!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var san = await _ctx.GetSANByID(CurrentSanIdleItem.ID);
                var planningSchedule = new PlanningSchedule(san) { Owner = this };
                planningSchedule.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при получении информации о выбранном хранилище данных!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы регистрации нового простоя хранилища данных
        /// </summary>
        private async void tsmi_SanRegisterIdle_Click(object sender, EventArgs e)
        {
            if (CurrentSanIdleItem == null)
            {
                MessageBox.Show("Выберите хранилище данных из списка для регистрации нового простоя!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var san = await _ctx.GetSANByID(CurrentSanIdleItem.ID);

                if (_ctx.GetServicesBySAN(san).Count == 0)
                {
                    MessageBox.Show("Данное хранилище данных не используется на данный момент! " +
                                    "\nРегистрация простоя невозможна!",
                        "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                var registerNewIdle = new RegisterNewIdle(san);
                if (registerNewIdle.ShowDialog() == DialogResult.OK)
                    BindCollections();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при получении информации о выбранной конфигурации сервера!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void EquipmentIdleMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            Owner?.Show();
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
                BindCollections();
            }
            catch
            {
            }
        }
    }
}
