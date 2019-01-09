using System;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;

namespace MTF_Services.WinForms.Forms.Employee
{
    /// <summary>
    /// Класс главной формы сотрудника.
    /// </summary>
    public partial class EmployeeForm : Form
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
        /// Выбранный сервис
        /// </summary>
        public UserService SelectedUserService { get; set; }

        /// <summary>
        /// Конструктор главной формы сотрудника.
        /// </summary>
        public EmployeeForm()
        {
            InitializeComponent();
            SubscribeMenuItems();
            FormClosing += EmployeeForm_FormClosing;

            _ctx = new Context();
            BindUsedServices();
            timer.Enabled = true;
        }

        /// <summary>
        /// Обработчик события закрытия формы.
        /// </summary>
        private void EmployeeForm_FormClosing(object sender, FormClosingEventArgs e)
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
        /// Привязка коллекции используемых сервисов сотрудника
        /// </summary>
        private void BindUsedServices()
        {
            userServiceBindingSource.DataSource = _ctx.GetUsedServices();
            dg_Service.DataSource = userServiceBindingSource;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы подписки на новые сервисы
        /// </summary>
        private void подписатьсяНаНовыйСервисToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var avalibleServiceTypes = _ctx.GetAvalibleServiceTypes();
                if (avalibleServiceTypes.Count == 0)
                {
                    MessageBox.Show("Отсутствуют предоставляемые сервисы на данный момент!", "Предепреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                var subscribeServicesForm = new SubscribeServicesForm(avalibleServiceTypes);
                if (subscribeServicesForm.ShowDialog() == DialogResult.OK)
                    BindUsedServices();
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при получении списка доступных сервисов!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который совершает отписку от выбранного сервиса
        /// </summary>
        private void отписатьсяОтВыбранногоСервисаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedUserService == null)
            {
                MessageBox.Show("Выберите сервис для отписки!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            var result = MessageBox.Show("Вы будете отписанны от выбранного сервиса! Продолжить?", "Предупреждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                try
                {
                    ServiceType serviceType = _ctx.GetServiceTypeById(SelectedUserService.ServiceTypeId);
                    _ctx.UnSubscribeSelectedService(serviceType);
                    MessageBox.Show("Изменения успешно сохранены!", "Информация", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    BindUsedServices();
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при отписке от выбранного сервиса!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных подписанных серверов
        /// </summary>
        private void userServiceBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as UserService;
            if (selectedItem != null)
                SelectedUserService = selectedItem;
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
                BindUsedServices();
            }
            catch
            {
            }
        }
    }
}
