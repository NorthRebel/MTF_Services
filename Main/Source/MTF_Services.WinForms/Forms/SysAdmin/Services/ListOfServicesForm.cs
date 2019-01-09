using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;

namespace MTF_Services.WinForms.Forms.SysAdmin.Services
{
    /// <summary>
    /// Класс формы списка сервисов
    /// </summary>
    public partial class ListOfServicesForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Текущая платформа
        /// </summary>
        public PlatformServiceUser SelectedPlatform { get; set; }

        /// <summary>
        /// Текущий сервис
        /// </summary>
        public ServiceDetailInfo SelectedService { get; set; }

        /// <summary>
        /// Список детального описания сервисов
        /// </summary>
        private BindingList<ServiceDetailInfo> ServicesDetailInfoMain { get; set; }

        /// <summary>
        /// Отобранный список детального описания сервисов 
        /// </summary>
        private BindingList<ServiceDetailInfo> ServicesDetailInfoToShow { get; set; }

        /// <summary>
        /// Конструктор формы списка сервисов
        /// </summary>
        public ListOfServicesForm()
        {
            InitializeComponent();
            SubscribeMenuItems();

            созданиеНовойПлатформыToolStripMenuItem.Click += picBtn_AddNewPlatform_Click;
            редактированиеВыбраннойПлатформыToolStripMenuItem.Click += picBtn_EditSelectedPlatform_Click;
            удалениеВыбраннойПлатформыToolStripMenuItem.Click += picBtn_DeleteSelectedPlatform_Click;

            созданиеНовогоСервисаToolStripMenuItem.Click += picBtn_AddNewService_Click;
            редактированиеВыбранногоСервисаToolStripMenuItem.Click += picBtn_EditSelectedService_Click;
            удалениеВыбранногоСервисаToolStripMenuItem.Click += picBtn_DeleteSelectedService_Click;

            tsmi_AddNewService.Click += picBtn_AddNewService_Click;
            tsmi_EditSelectedService.Click += picBtn_EditSelectedService_Click;
            tsmi_DeleteSelectedService.Click += picBtn_DeleteSelectedService_Click;
            tsmi_ShowAllServices.Click += ttmi_ShowAllServices_Click;

            tsmi_AddNewPlatfrom.Click += picBtn_AddNewPlatform_Click;
            tsmi_EditSelectedPlatform.Click += picBtn_EditSelectedPlatform_Click;
            tsmi_DeleteSelectedPlatform.Click += picBtn_DeleteSelectedPlatform_Click;
            tsmi_ShowServicesBySelectedPlatform.Click += ttmi_ShowServicesBySelectedPlatform_Click;

            _ctx = new Context();
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
        /// Инициализация привязки списка платформ с информацией о количестве пользователей и сервисов
        /// </summary>
        private void BindPlatformList()
        {
            platformServiceUserBindingSource.DataSource = _ctx.GetPlatformServiceUsers();
            dg_Platforms.DataSource = platformServiceUserBindingSource;
        }        

        /// <summary>
        /// Инициализация привязки полного списка конфигураций сервисов с детальным описанием
        /// </summary>
        private void BindServicesList()
        {
            ServicesDetailInfoMain = _ctx.GetServicesDetailInfo();
            serviceDetailInfoBindingSource.DataSource = ServicesDetailInfoMain;
            dg_Services.DataSource = serviceDetailInfoBindingSource;
            UpdateStatBar();
        }

        /// <summary>
        /// Обновление строки состояния
        /// </summary>
        private void UpdateStatBar()
        {
            lbl_PlatformsCount.Text = $"Кол-во платформ: {dg_Platforms.Rows.Count}";
            lbl_ServiceTotalCount.Text = $"Общее кол-во конфигураций сервисов: {ServicesDetailInfoMain?.Count ?? 0}";
            lbl_ServiceSelectedCount.Text = $"Отобрано конфигураций: {dg_Services.Rows.Count}";
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных списка платформ
        /// </summary>
        private void platformServiceUserBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as PlatformServiceUser;
            if (selectedItem != null)
            {
                SelectedPlatform = selectedItem;
                if (ServicesDetailInfoMain != null)
                {
                    ServicesDetailInfoToShow = new BindingList<ServiceDetailInfo>(ServicesDetailInfoMain
                        .Where(sdi => sdi.PaasType.Equals(SelectedPlatform.Name)).ToList());
                    serviceDetailInfoBindingSource.DataSource = ServicesDetailInfoToShow;
                    dg_Services.DataSource = serviceDetailInfoBindingSource;
                    dg_Services.Columns[1].Visible = false;
                    UpdateStatBar();
                }
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных списка сервисов
        /// </summary>
        private void serviceDetailInfoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as ServiceDetailInfo;
            if (selectedItem != null)
                SelectedService = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который совершает переход на форму создания новой платформы
        /// </summary>
        private void picBtn_AddNewPlatform_Click(object sender, EventArgs e)
        {
            if (!_ctx.CheckInfrastructureToCreatePlatform())
            {
                MessageBox.Show("Имеется неполные данные об вычислительной инфраструктруре!" +
                                "\nПеред тем как добавить платформу заполните сведения об конфигурациях серверов и хранилищ данных!",
                    "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var editPlatformForm = new EditPlatformForm { Owner = this };
            editPlatformForm.Show();
            Hide();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который совершает переход на форму редактирования выбранной платформы
        /// </summary>
        private async void picBtn_EditSelectedPlatform_Click(object sender, EventArgs e)
        {
            if (SelectedPlatform == null)
            {
                MessageBox.Show("Выберите платформу для редактирования!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                PaasType platformToEdit = await _ctx.GetPlatformByPlatformServiceUser(SelectedPlatform);
                var editPlatformForm = new EditPlatformForm(platformToEdit) { Owner = this };
                editPlatformForm.Show();
                Hide();
            }
            catch
            {
                MessageBox.Show("Не удалось получить данные по выбранной платформе!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление выбранной платформы
        /// </summary>
        private async void picBtn_DeleteSelectedPlatform_Click(object sender, EventArgs e)
        {
            if (SelectedPlatform == null)
            {
                MessageBox.Show("Выберите платформу для удаления!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Выбранная платформа и все ее сервисы будут удалены! Продолжить?",
                "Удаление платформы", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                try
                {
                    PaasType platformToDel = await _ctx.GetPlatformByPlatformServiceUser(SelectedPlatform);
                    await _ctx.DeletePaasType(platformToDel);
                    BindPlatformList();
                    BindServicesList();
                    MessageBox.Show("Платформа и все ее данные успешно удалены!", "Информация", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Не удалось удалить выбранную платформу!", "Ошибка при удалении",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который совершает переход на форму создания нового сервиса
        /// </summary>
        private async void picBtn_AddNewService_Click(object sender, EventArgs e)
        {
            if (_ctx.CheckActivePaas() == 0)
            {
                MessageBox.Show("Отсутсвуют платформы, на которые были распределены вычислительные ресурсы!", "Ошибка",
                    MessageBoxButtons.OK);
                return;
            }

            EditServiceForm editServiceForm = null;

            if (SelectedPlatform != null)
            {
                try
                {
                    PaasType selPlatf = await _ctx.GetPlatformByPlatformServiceUser(SelectedPlatform);
                    if (selPlatf.Server.Count > 0 && selPlatf.SAN.Count > 0)
                        editServiceForm = new EditServiceForm(selPlatf);
                    else
                    {
                        var result = MessageBox.Show(
                            "У выбранной платформы отсутствуют привязанные конфигурации серверов и/или хранилища данных! Открыть редактор сервиса?", "Предупреждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        if (result == DialogResult.Yes)
                            editServiceForm = new EditServiceForm();
                        else
                            return;
                    }
                }
                catch
                {
                    var result = MessageBox.Show(
                        "Произошла ошибка при получении типа выбранной платформы! Открыть редактор сервиса?", "Ошибка",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (result == DialogResult.Yes)
                        editServiceForm = new EditServiceForm();
                    else
                        return;
                }
            }
            else
                editServiceForm = new EditServiceForm();

            editServiceForm.Owner = this;
            editServiceForm.Show();
            Hide();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который совершает переход на форму редактирования выбранного сервиса
        /// </summary>
        private async void picBtn_EditSelectedService_Click(object sender, EventArgs e)
        {
            if (SelectedService == null)
            {
                MessageBox.Show("Выберите сервис для редактирования!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Service serviceToEdit = await _ctx.GetServiceByServiceDetailInfo(SelectedService);
                var editServiceForm = new EditServiceForm(serviceToEdit) { Owner = this };
                editServiceForm.Show();
                Hide();
            }
            catch
            {
                MessageBox.Show("Не удалось получить информацию по выбранному сервису!", "Ошибка при удалении",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление выбранного сервиса
        /// </summary>
        private async void picBtn_DeleteSelectedService_Click(object sender, EventArgs e)
        {
            if (SelectedService == null)
            {
                MessageBox.Show("Выберите сервис для удаления!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show("Выбранная серсвис будет удален! Продолжить?",
                "Удаление сервиса", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Service serviceToDel = await _ctx.GetServiceByServiceDetailInfo(SelectedService);
                    await _ctx.DeleteService(serviceToDel);
                    BindPlatformList();
                    BindServicesList();
                    MessageBox.Show("Сервис успешно удален!", "Информация", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Не удалось удалить выбранный сервис!", "Ошибка при удалении",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который производит просмотр всех конфигураций сервисов
        /// </summary>
        private void ttmi_ShowAllServices_Click(object sender, EventArgs e)
        {
            BindServicesList();
            dg_Services.Columns[1].Visible = true;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который производит отбор сервисов по выбранной платформе
        /// </summary>
        private void ttmi_ShowServicesBySelectedPlatform_Click(object sender, EventArgs e)
        {
            var selectedItem = platformServiceUserBindingSource.Current as PlatformServiceUser;
            if (selectedItem != null)
            {
                SelectedPlatform = selectedItem;
                if (ServicesDetailInfoMain != null)
                {
                    ServicesDetailInfoToShow = new BindingList<ServiceDetailInfo>(ServicesDetailInfoMain
                        .Where(sdi => sdi.PaasType.Equals(SelectedPlatform.Name)).ToList());
                    serviceDetailInfoBindingSource.DataSource = ServicesDetailInfoToShow;
                    dg_Services.DataSource = serviceDetailInfoBindingSource;
                    dg_Services.Columns[1].Visible = false;
                    UpdateStatBar();
                }
            }
            else
                MessageBox.Show("Выберите платформу из списка!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события активации формы
        /// </summary>
        private void ListOfServicesForm_Activated(object sender, EventArgs e)
        {
            BindPlatformList();
            BindServicesList();
        }
        
        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void ListOfServicesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Owner?.Show();
        }

        /// <summary>
        /// Обработчик события нажатий клавиш клавиатуры на форме
        /// </summary>
        private void ListOfServicesForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
