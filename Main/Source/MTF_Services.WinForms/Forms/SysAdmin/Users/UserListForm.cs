using System;
using System.ComponentModel;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;

namespace MTF_Services.WinForms.Forms.SysAdmin.Users
{
    /// <summary>
    /// Класс формы редактирования пользователей
    /// </summary>
    public partial class UserListForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Текущий пользователь
        /// </summary>
        public UserInfo CurrentUser { get; set; }

        /// <summary>
        /// Список пользователей
        /// </summary>
        public BindingList<UserInfo> UsersInfoMain { get; set; }

        /// <summary>
        /// Отобранный список пользователей
        /// </summary>
        public BindingList<UserInfo> UsersInfoToShow { get; set; }

        /// <summary>
        /// Конструктор формы редактирования пользователей
        /// </summary>
        public UserListForm()
        {
            InitializeComponent();
            SubscribeMenuItems();

            добавитьToolStripMenuItem.Click += новыйПользовательToolStripMenuItem_Click;
            редактироватьToolStripMenuItem1.Click += редактироватьToolStripMenuItem_Click;
            удалитьToolStripMenuItem1.Click += удалитьToolStripMenuItem_Click;

            _ctx = new Context();

            InitBindings();
        }

        /// <summary>
        /// Инициализация привязок данных
        /// </summary>
        private void InitBindings()
        {
            UsersInfoMain = _ctx.GetUsersInfo();
            UsersInfoToShow = null;
            
            userInfoBindingSource.DataSource = UsersInfoMain;
            dataGridView1.DataSource = userInfoBindingSource;
            dataGridView1.Refresh();
            UpdateStatBar();
        }

        /// <summary>
        /// Обновление строки состояния
        /// </summary>
        private void UpdateStatBar()
        {
            lbl_UserCount.Text = $"Общее кол-во пользователей: {UsersInfoMain?.Count ?? 0}";
            lbl_SelectedCount.Text = $"Отобрано записей: {dataGridView1.Rows.Count}";
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
        /// Обработчик события изменения текущего элемента в источнике данных пользователей
        /// </summary>
        private void userInfoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as UserInfo;
            if (selectedItem != null)
                CurrentUser = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы регистрации нового пользователя
        /// </summary>
        private void новыйПользовательToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editUsersForm = new EditUsersForm();
            if (editUsersForm.ShowDialog() == DialogResult.OK)
                InitBindings();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно формы редактирования выбранного пользователя
        /// </summary>
        private async void редактироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selUsr = await _ctx.GetUserByUserInfo(CurrentUser);
            if (selUsr != null)
            {
                var editUsersForm = new EditUsersForm(selUsr);
                if (editUsersForm.ShowDialog() == DialogResult.OK)
                {
                    InitBindings();
                    if (CurrentUser != null)
                    {
                        int pos = userInfoBindingSource.IndexOf(CurrentUser);
                        if (pos > -1)
                            userInfoBindingSource.Position = pos;
                    }
                }
            }
            else
                MessageBox.Show("Выберите пользователя из списка для его редактирования!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет удаление выбранного пользователя
        /// </summary>
        private async void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selUsr = await _ctx.GetUserByUserInfo(CurrentUser);
            if (selUsr != null)
            {
                var result = MessageBox.Show("Выбранный пользователь и вся информация о нем будет удалена! Продолжить?",
                    "Удаление пользователя", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (result == DialogResult.No)
                    return;

                try
                {
                    await _ctx.DeleteUser(selUsr);
                    InitBindings();
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при удалении учетной записи!", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Выберите пользователя из списка для его редактирования!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        
        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который выполняет просмотр всех зарегистрированных пользователей
        /// </summary>
        private void всеЗаписиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitBindings();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на элемент главного меню, 
        /// который открывает диалоговое окно задания критериев для отбора зарегистрированных пользователей
        /// </summary>
        private void отборToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var userConditionForm = new UserConditionForm(UsersInfoMain);
            if (userConditionForm.ShowDialog() == DialogResult.OK)
            {
                UsersInfoToShow = userConditionForm.UsersInfoToShow;
                userInfoBindingSource.DataSource = UsersInfoToShow;
                dataGridView1.DataSource = userInfoBindingSource;
                UpdateStatBar();
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиш клавиатуры на форме
        /// </summary>
        private void UserListForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
