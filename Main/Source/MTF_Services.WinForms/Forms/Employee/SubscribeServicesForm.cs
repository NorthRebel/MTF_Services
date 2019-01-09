using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.Employee
{
    /// <summary>
    /// Класс формы подписки сотрудника на новые сервисы
    /// </summary>
    public partial class SubscribeServicesForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Доступные  сервисы
        /// </summary>
        public BindingList<ServiceType> AvalibleServices { get; set; }

        /// <summary>
        /// Выбранные сервисы на подписку
        /// </summary>
        public BindingList<ServiceType> UsedServices { get; set; }

        /// <summary>
        /// Выбранный использованный сервис
        /// </summary>
        public ServiceType SelectedUsedServiceType { get; set; }

        /// <summary>
        /// Выбранный доступный сервис
        /// </summary>
        public ServiceType SelectedAvalibleServiceType { get; set; }

        /// <summary>
        /// Конструктор формы подписки сотрудника на новые сервисы
        /// </summary>
        /// <param name="avalibleServiceTypes">Коллекция доступных типов сервисов</param>
        public SubscribeServicesForm(BindingList<ServiceType> avalibleServiceTypes)
        {
            InitializeComponent();

            btn_OK.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));

            _ctx = new Context();
            BindCollections(avalibleServiceTypes);
        }

        /// <summary>
        /// Привязка коллекций
        /// </summary>
        private void BindCollections(BindingList<ServiceType> avalibleServiceTypes)
        {
            AvalibleServices = avalibleServiceTypes;
            AvalibleServiceTypeBS.DataSource = avalibleServiceTypes;
            lst_AvalibleServices.DataSource = AvalibleServiceTypeBS;

            UsedServices = new BindingList<ServiceType>();
            UsedServiceTypeBS.DataSource = UsedServices;
            lst_UsedServices.DataSource = UsedServiceTypeBS;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных доступных сервисов
        /// </summary>
        private void AvalibleServiceTypeBS_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as ServiceType;
            if (selectedItem != null)
                SelectedAvalibleServiceType = selectedItem;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных выбранных сервисов
        /// </summary>
        private void UsedServiceTypeBS_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as ServiceType;
            if (selectedItem != null)
                SelectedUsedServiceType = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который совершает добавление к выбранным сервисам
        /// </summary>
        private void btn_AddToUsed_Click(object sender, EventArgs e)
        {
            if (SelectedAvalibleServiceType == null || lst_AvalibleServices.SelectedIndex < 0)
            {
                MessageBox.Show("Доступный сервис не выбран!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            UsedServices.Add(SelectedAvalibleServiceType);
            AvalibleServices.Remove(SelectedAvalibleServiceType);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который совершает удаление из выбранных сервисов
        /// </summary>
        private void btn_RemoveFromUsed_Click(object sender, EventArgs e)
        {
            if (SelectedUsedServiceType == null || lst_UsedServices.SelectedIndex < 0)
            {
                MessageBox.Show("Используемый сервис не выбран!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            AvalibleServices.Add(SelectedUsedServiceType);
            UsedServices.Remove(SelectedUsedServiceType);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену изменений, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void SubscribeServicesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UsedServices.Count > 0 && DialogResult != DialogResult.OK)
            {
                var result = MessageBox.Show("Изменения не будут сохранены! Продолжить?", "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который выполняет сохранение изменений
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (UsedServices.Count == 0)
            {
                MessageBox.Show("Отсутствуют выбранные сервисы!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                _ctx.SubscribeToSelectedServices(UsedServices.ToList());
                UsedServices.Clear();
                MessageBox.Show("Новые подписки на сервисы успешно сохранены!", "Информация", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                MessageBox.Show("Не удалось подписаться на выбранные сервисы!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
