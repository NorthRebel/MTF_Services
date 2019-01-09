using System;
using System.Linq;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;

namespace MTF_Services.WinForms.Forms
{
    /// <summary>
    /// Класс формы просмотра данных текущей учетной записи.
    /// </summary>
    public partial class ShowUserProfileForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Символ для скрытия пароля.
        /// </summary>
        private readonly char _passwordCh;

        private string _loginBeforeChange;

        /// <summary>
        /// Конструктор формы просмотра данных текущей учетной записи.
        /// </summary>
        public ShowUserProfileForm()
        {
            InitializeComponent();
            _passwordCh = textBox2.PasswordChar;

            _ctx = new Context();

            _loginBeforeChange = Context.CurrentUser.Login;
            user.DataSource = Context.CurrentUser;
            user.ResumeBinding();
            var bindingList = _ctx.GetUsersInfo();
            var x = bindingList.Single(s => s.TabNo == Context.CurrentUser.TabNo);
            userInfo.DataSource = x;
        }

        /// <summary>
        /// Обработчик события перемещения курсора мыши на метке "Пароль",
        /// который отображает пароль
        /// </summary>
        private void label2_MouseHover(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '\0';
        }

        /// <summary>
        /// Обработчик события выхода курсора мыши на метке "Пароль",
        /// который скрывает пароль
        /// </summary>
        private void label2_MouseLeave(object sender, EventArgs e)
        {
            textBox2.PasswordChar = _passwordCh;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var CurrentUser = user.DataSource as User;

            try
            {
                if (string.IsNullOrWhiteSpace(CurrentUser.Login) || string.IsNullOrWhiteSpace(CurrentUser.Password))
                {
                    MessageBox.Show("Введите все поля для сохранения изменений!", "Предупреждение",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                bool existed = false;
                existed = await _ctx.CheckUserLoginForDublicate(CurrentUser.Login) && !CurrentUser.Login.Equals(_loginBeforeChange);

                if (existed)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                await _ctx.SaveChangesAsync();
                _loginBeforeChange = CurrentUser.Login;
                MessageBox.Show("Изменения успешно сохранены!", "Информация", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при сохранении изменений!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
