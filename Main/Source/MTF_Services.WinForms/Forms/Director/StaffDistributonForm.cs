using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.Director
{
    /// <summary>
    /// Класс формы рапсределения персонала, задействованного в простое сервиса
    /// </summary>
    public partial class StaffDistributonForm : Form
    {
        /// <summary>
        /// Выбранный экземпляр простоя сервиса
        /// </summary>
        public ServiceIdle SelectedServiceIdle { get; set; }

        /// <summary>
        /// Доступный персонал
        /// </summary>
        public BindingList<User> AvaliblePersonal { get; set; }

        /// <summary>
        /// Задействованный персонал
        /// </summary>
        public BindingList<User> UsedPersonal { get; set; }

        /// <summary>
        /// Выбранный доступный сотрудник
        /// </summary>
        public User SelectedAvalibleUser { get; set; }

        /// <summary>
        /// Выбранный задействованный сотрудник
        /// </summary>
        public User SelectedUsedUser { get; set; }

        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Конструктор формы распределения  персонала, задействованного в простое сервиса
        /// </summary>
        /// <param name="selectedServiceIdle">Выбранный экземпляр простоя</param>
        public StaffDistributonForm(ServiceIdle selectedServiceIdle)
        {
            SelectedServiceIdle = selectedServiceIdle;
            InitializeComponent();

            btn_OK.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));
            _ctx = new Context();
            BindCollections();
        }

        /// <summary>
        /// Привязка коллекций
        /// </summary>
        private void BindCollections()
        {
            var adminsList = _ctx.GetAdminsList();
            var users = SelectedServiceIdle.User.ToList();
            users.ForEach(usr =>
            {
                if (adminsList.Contains(usr))
                    adminsList.Remove(usr);
            });
            
            AvaliblePersonal = new BindingList<User>(adminsList);
            UsedPersonal = new BindingList<User>(users);

            AvalibleUserBindingSource.DataSource = AvaliblePersonal;
            UsedUserBindingSource.DataSource = UsedPersonal;

            dg_AvalibleUser.DataSource = AvalibleUserBindingSource;
            dg_UsedUser.DataSource = UsedPersonal;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных доступного персонала
        /// </summary>
        private void AvalibleUserBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as User;
            if (selectedItem != null)
                SelectedAvalibleUser = selectedItem;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных задействованного персонала
        /// </summary>
        private void UsedUserBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as User;
            if (selectedItem != null)
                SelectedUsedUser = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который осуществляет удаление из выбранных сервисов
        /// </summary>
        private void btn_RemoveFromUsed_Click(object sender, EventArgs e)
        {
            if (SelectedUsedUser == null || dg_UsedUser.CurrentRow != null)
            {
                MessageBox.Show("Пользователь не выбран!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            AvaliblePersonal.Add(SelectedUsedUser);
            UsedPersonal.Remove(SelectedUsedUser);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который осуществляет добавление к выбранным сервисам
        /// </summary>
        private void btn_AddToUsed_Click(object sender, EventArgs e)
        {
            if (SelectedAvalibleUser == null || dg_AvalibleUser.CurrentRow != null)
            {
                MessageBox.Show("Пользователь не выбран!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            UsedPersonal.Add(SelectedAvalibleUser);
            AvaliblePersonal.Remove(SelectedAvalibleUser);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену подписки на новые сервисы, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который выполняет сохранение изменений
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (UsedPersonal.Count == 0)
            {
                MessageBox.Show("Добавьте к задействованному персоналу сотурдников", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                SelectedServiceIdle.User.Clear();
                UsedPersonal.ToList().ForEach(up => SelectedServiceIdle.User.Add(up));                
                _ctx.UpdateServiceIdle(SelectedServiceIdle);
                DialogResult = DialogResult.OK;
                MessageBox.Show("Изменения успешно сохранены!", "Информация", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Close();
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить изменения!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void StaffDistributonForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                var result = MessageBox.Show("Изменения не будут сохранены! Продолжить?", "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }
    }
}
