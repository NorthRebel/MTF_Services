using System;
using System.ComponentModel;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Forms.SysAdmin.EquipmentIdle;
using MTF_Services.WinForms.Forms.SysAdmin.Infrastructure;
using MTF_Services.WinForms.Forms.SysAdmin.Services;
using MTF_Services.WinForms.Forms.SysAdmin.Users;

namespace MTF_Services.WinForms.Forms.SysAdmin
{
    /// <summary>
    /// Класс главной формы системного администратора.
    /// </summary>
    public partial class SysAdminForm : Form
    {
        #region StatBar_Labels

        private const string USED_PLATFORMS_COUNT_LBL = "Кол-во используемых платформ:";
        private const string USED_SERVICE_COUNT_LBL = "Кол-во предоставляемых сервисов:";
        private const string SERVICE_CONFIG_COUNT_LBL = "Кол-во конфигураций сервисов:";

        private const string ZERO_LBL = "0";

        #endregion

        /// <summary>
        /// Флаг совершенного выхода из учетной записи
        /// </summary>
        private bool _logout;

        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Конструктор главной формы системного администратора.
        /// </summary>
        public SysAdminForm()
        {
            InitializeComponent();
            SubscribeMenuItems();
            FormClosing += SysAdminForm_FormClosing;

            _ctx = new Context();
            timer.Enabled = true;
        }

        /// <summary>
        /// Обновление привязок данных.
        /// </summary>
        private async void UpdateBindings()
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
                var paasInfos = await _ctx.GetPaasInfo();
                paasInfoBindingSource.DataSource = paasInfos;
                lbl_usedPlatformsCount.Text = $"{USED_PLATFORMS_COUNT_LBL} {_ctx.UsedPlatformsCount(paasInfos)}";

                var serviceInfos = _ctx.GetServiceInfo();
                serviceInfoBindingSource.DataSource = serviceInfos;
                lbl_usedServiceCount.Text = $"{USED_SERVICE_COUNT_LBL} {_ctx.UsedServiceCount(serviceInfos)}";
                lbl_serviceConfigCount.Text = $"{SERVICE_CONFIG_COUNT_LBL} {_ctx.ServiceConfigsCount(serviceInfos)}";
            }
            catch
            {
                lbl_usedPlatformsCount.Text = $"{USED_PLATFORMS_COUNT_LBL} {ZERO_LBL}";
                lbl_usedServiceCount.Text = $"{USED_SERVICE_COUNT_LBL} {ZERO_LBL}";
                lbl_serviceConfigCount.Text = $"{SERVICE_CONFIG_COUNT_LBL} {ZERO_LBL}";
               // MessageBox.Show("Ошибка при получении данных!");
            }
            finally
            {
                UpdateLabels();
            }

        }

        /// <summary>
        /// Скрытие таблицы с информацией о сервисах.
        /// </summary>
        public void HideServiceGrid()
        {
            dataGridView3.Visible = false;
            dataGridView3.Dock = DockStyle.None;
            label9.Visible = true;
            label9.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// Скрытие таблицы с информацией о платформах.
        /// </summary>
        private void HidePaasGrid()
        {
            dataGridView2.Visible = false;
            dataGridView2.Dock = DockStyle.None;
            label10.Visible = true;
            label10.Dock = DockStyle.Fill;
        }

        /// <summary>
        /// Обновление таблиц и меток, которые сообщают об отсутствии информации.
        /// </summary>
        private void UpdateLabels()
        {
            try
            {
                if (serviceInfoBindingSource.DataSource == null ||
                    (serviceInfoBindingSource.DataSource as BindingList<ServiceInfo>).Count == 0)
                    HideServiceGrid();
                else
                {
                    dataGridView3.Visible = true;
                    dataGridView3.Dock = DockStyle.Fill;
                    label9.Visible = false;
                    label9.Dock = DockStyle.None;
                }
            }
            catch
            {
                HideServiceGrid();
            }

            try
            {
                if (paasInfoBindingSource.DataSource == null ||
                    (paasInfoBindingSource.DataSource as BindingList<PaasInfo>).Count == 0)
                    HidePaasGrid();
                else
                {
                    dataGridView2.Visible = true;
                    dataGridView2.Dock = DockStyle.Fill;
                    label10.Visible = false;
                    label10.Dock = DockStyle.None;
                }
            }
            catch
            {
                HidePaasGrid();
            }
        }

        /// <summary>
        /// Обработчик события закрытия формы.
        /// </summary>
        private void SysAdminForm_FormClosing(object sender, FormClosingEventArgs e)
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
        /// Обработчик события наведения курсора на элемент главного меню,
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
        /// который выполняет переход на форму редактирования инфраструктуры
        /// </summary>
        private void редактированиеИнфраструктурыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EditInfrastructureFrom { Owner = this }.Show();
            Hide();
        }


        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет переход в меню простоя инфраструктуры
        /// </summary>
        private void простойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var equipmentIdleMenu = new EquipmentIdleMenu() { Owner = this};
            equipmentIdleMenu.Show();
            Hide();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы редактирования учетных записей пользователей
        /// </summary>
        private void редактированиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new UserListForm().ShowDialog();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы регистрации новой учетной записи
        /// </summary>
        private void регистрацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EditUsersForm().ShowDialog();
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
        /// который выполняет переход на форму создания новой платформы
        /// </summary>
        private void добавитьНовуюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_ctx.CheckInfrastructureToCreatePlatform())
            {
                MessageBox.Show("Имеется неполные данные об вычислительной инфраструктруре!" +
                                "\nПеред тем как добавить платформу заполните сведения об конфигурациях серверов и хранилищ данных!",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var editPlatformForm = new EditPlatformForm() { Owner = this };
            editPlatformForm.Show();
            Hide();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет переход на форму редактирования выбранной платформы
        /// </summary>
        private async void редактироватьВыбраннуюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedPlatform = paasInfoBindingSource.Current as PaasInfo;
            if (selectedPlatform == null)
            {
                MessageBox.Show("Платформа не выбрана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PaasType paasTypeToEdit = null;

            try
            {
                paasTypeToEdit = await _ctx.GetPaasFromPaasInfo(selectedPlatform);
            }
            catch
            {
                MessageBox.Show("Не удалось получить данные по выбранной платформе!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var editPlatformForm = new EditPlatformForm(paasTypeToEdit) { Owner = this };
            editPlatformForm.Show();
            Hide();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет переход на форму создания нового сервиса
        /// </summary>
        private void добавитьНовыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_ctx.CheckActivePaas() == 0)
            {
                MessageBox.Show("Отсутсвуют платформы, на которые были распределены вычислительные ресурсы!", "Ошибка",
                    MessageBoxButtons.OK);
                return;
            }

            var editServiceForm = new EditServiceForm() { Owner = this };
            editServiceForm.Show();
            Hide();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет удаление выбранной категории сервиса
        /// </summary>
        private async void удалитьВыбранныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedServiceInfo = serviceInfoBindingSource.Current as ServiceInfo;
            if (selectedServiceInfo == null)
            {
                MessageBox.Show("Выберите категорию сервиса, которую следует удалить!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                var serviceType = await _ctx.GetPaasTypeByName(selectedServiceInfo.Name);

                DialogResult result = DialogResult.No;
                if (serviceType.Service.Count == 0)
                    result = MessageBox.Show("Выбранная категория сервисов будет удалена! Продолжить?",
                        "Удаление категории сервиса", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                else
                    result = MessageBox.Show("Выбранная категория сервисов и все ее конфигурации будет удалены! Продолжить?",
                        "Удаление категории сервиса", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _ctx.DeleteServiceType(serviceType);
                        UpdateBindings();
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка при удалении категории сервисов!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при получении информации о категории сервисов и ее конфигурациях!",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события активации формы
        /// </summary>
        private void SysAdminForm_Activated(object sender, EventArgs e)
        {
            UpdateBindings();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет удаление выбранной платформы
        /// </summary>
        private async void удалитьВыбраннуюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selectedPlatform = paasInfoBindingSource.Current as PaasInfo;
            if (selectedPlatform == null)
            {
                MessageBox.Show("Платформа не выбрана!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var paasTypeToDel = await _ctx.GetPaasFromPaasInfo(selectedPlatform);

                DialogResult result = DialogResult.No;
                if (paasTypeToDel.Service.Count == 0)
                    result = MessageBox.Show("Выбранная платформа будет удалена! Продолжить?",
                        "Удаление категории платформы", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                else
                    result = MessageBox.Show("Выбранная платформа и все ее конфигурации сервисов будет удалены! Продолжить?",
                        "Удаление категории платформы", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        await _ctx.DeletePaasType(paasTypeToDel);
                        UpdateBindings();
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка при удалении платформы!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось получить данные по выбранной платформе!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет переход на форму редактора платформ и сервисов
        /// </summary>
        private void редакторПлатформИСервисовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var listOfServicesForm = new ListOfServicesForm() {Owner = this};
            listOfServicesForm.Show();
            Hide();
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
    }
}
