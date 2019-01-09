using System;
using System.ComponentModel;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;

namespace MTF_Services.WinForms.Forms.Director
{
    /// <summary>
    /// Класс главной формы директора.
    /// </summary>
    public partial class DirectorForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Флаг совершенного выхода из учетной записи
        /// </summary>
        private bool _logout;

        /// <summary>
        /// Выбранная заявка на создание сервиса
        /// </summary>
        public ServiceRequestItem SelectedServiceRequest { get; set; }

        /// <summary>
        /// Выбранный простой сервиса
        /// </summary>
        public IdleItem SelectedIdleItem { get; set; }

        /// <summary>
        /// Конструктор главной формы директора.
        /// </summary>
        public DirectorForm()
        {
            InitializeComponent();
            SubscribeMenuItems();
            FormClosing += DirectorForm_FormClosing;

            _ctx = new Context();
            BindAll();
            timer.Enabled = true;
        }

        /// <summary>
        /// Обработчик события закрытия формы.
        /// </summary>
        private void DirectorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall || _logout)
                return;

            var result = MessageBox.Show("Выберите дальнейшее действие:" +
                                         "\n\tДа - сменить пользователя;" +
                                         "\n\tНет - закрыть приложение;" +
                                         "\n\tОтмена - остаться в текущем окне",
                "Выход из основного меню",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Asterisk,
                MessageBoxDefaultButton.Button3);
            switch (result)
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                case DialogResult.Yes:
                    _ctx.Logout();
                    Owner?.Show();
                    break;
                case DialogResult.No:
                    Application.Exit();
                    break;
            }
        }

        #region Bindings

        /// <summary>
        /// Привязка всех коллекций
        /// </summary>
        private void BindAll()
        {
            BindServiceInfo();
            BindServiceRequest();
            BindServiceCost();
            BindPlatformList();
            BindUsersInformation();
            BindServiceIdle();
        }

        /// <summary>
        /// Привязка коллекции с обзором сервисов
        /// </summary>
        private void BindServiceInfo()
        {
            serviceInfoBindingSource.DataSource = _ctx.GetServiceInfo();
            dg_ServiceInfo.DataSource = serviceInfoBindingSource;
            dg_ServiceInfo.Refresh();
        }

        /// <summary>
        /// Привязка коллекции с заявками на создание сервиса
        /// </summary>
        private void BindServiceRequest()
        {
            serviceRequestItemBindingSource.DataSource = _ctx.GetServiceReuqests();
            dg_ServiceRequest.DataSource = serviceRequestItemBindingSource;
            dg_ServiceRequest.Refresh();
        }

        /// <summary>
        /// Получение коллекции со стоимостью сервисов
        /// </summary>
        private void BindServiceCost()
        {
            serviceDetailInfoBindingSource.DataSource = _ctx.GetServicesCost();
            dg_ServiceDetailInfo.DataSource = serviceDetailInfoBindingSource;
            dg_ServiceDetailInfo.Refresh();
        }

        /// <summary>
        /// Инициализация привязки списка платформ с информацией о количестве пользователей и сервисов
        /// </summary>
        private void BindPlatformList()
        {
            platformServiceUserBindingSource.DataSource = _ctx.GetPlatformServiceUsers();
            dg_Platforms.DataSource = platformServiceUserBindingSource;
            dg_Platforms.Refresh();
        }

        /// <summary>
        /// Привязка списка с информацией пользователей
        /// </summary>
        private void BindUsersInformation()
        {
            userInfoBindingSource.DataSource = _ctx.GetUsersInfo();
            dg_Users.DataSource = userInfoBindingSource;
            dg_Users.Refresh();
        }

        /// <summary>
        /// Привязка списка с информацией о простое сервисов
        /// </summary>
        private void BindServiceIdle()
        {
            idleItemBindingSource.DataSource = _ctx.GetIdleItems();
            dg_Idle.DataSource = idleItemBindingSource;
            dg_Idle.Refresh();
        }

        #endregion

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
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы просмотра и редактирования текущей учетной записи
        /// </summary>
        private void просмотрПрофиляToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ShowUserProfileForm().ShowDialog();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который совершает выход из текущей учетной записи
        /// </summary>
        private void сменитьПользователяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Будет произведен выход из текущей учетной записи! Продолжить?",
                "Смена пользователя",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                _ctx.Logout();
                Owner?.Show();
                _logout = true;
                this.Close();
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который завершает работу приложения
        /// </summary>
        private void завершениеРаботыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Приложение будет закрыто! Продолжить?",
                "Закрытие приложения",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
                Application.Exit();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы обработки выбранной заявки
        /// </summary>
        private void обработкаВыбраннойЗаявкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedServiceRequest == null)
            {
                MessageBox.Show("Выберите заявку на создание сервиса", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                Service selectedService = _ctx.GetServiceByID(SelectedServiceRequest.Id);
                var serviceRequestTreatment = new ServiceRequestTreatment(selectedService, SelectedServiceRequest);
                if (serviceRequestTreatment.ShowDialog() == DialogResult.OK)
                    BindAll();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при получении информации о выбранном сервисе!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных заявок на создание сервиса
        /// </summary>
        private void serviceRequestItemBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as ServiceRequestItem;
            if (selectedItem != null)
                SelectedServiceRequest = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно предварительного просмотра отчета по стоимости предоставления сервисов
        /// </summary>
        private void стоимостьПредоставленияСервисовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var headerText = dg_ServiceDetailInfo.Columns[5].HeaderText;
                BindingList<ServiceDetailInfo> sdi = (BindingList<ServiceDetailInfo>) serviceDetailInfoBindingSource.DataSource;

                if (sdi.Count == 0)
                {
                    MessageBox.Show("Отсутствуют предоставляемые сервисы!");
                    return;

                }

                var reportingForm = new ReportingForm(sdi, headerText);
                reportingForm.ShowDialog();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при создании отчета!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных заявок простоев сервисов
        /// </summary>
        private void idleItemBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as IdleItem;
            if (selectedItem != null)
                SelectedIdleItem = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы редактирования выбранного простоя
        /// </summary>
        private void редактированиеВыбранногоПростояToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedIdleItem == null)
            {
                MessageBox.Show("Выберите простой сервиса", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                ServiceIdle selectedServiceIdle = _ctx.GetServiceIdleByID(SelectedIdleItem.Id);
                var editIdleItemForm = new EditIdleItemForm(selectedServiceIdle);
                if (editIdleItemForm.ShowDialog() == DialogResult.OK)
                    BindServiceIdle();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при получении информации о выбранном простое сервиса!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                BindAll();
            }
            catch
            {
            }
        }
    }
}
