using System;
using System.Drawing;
using System.Windows.Forms;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Forms.Director;
using MTF_Services.WinForms.Forms.Employee;
using MTF_Services.WinForms.Forms.SysAdmin;

namespace MTF_Services.WinForms.Forms
{
    /// <summary>
    /// Класс формы авторизации пользователя.
    /// </summary>
    public partial class LoginForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Символ для скрытия пароля.
        /// </summary>
        private char passwordChar;

        /// <summary>
        /// Конструктор формы авторизации пользователя.
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            textBox1.KeyPress += TextBox1_KeyPress;
            textBox2.KeyPress += TextBox1_KeyPress;
            FormClosing += LoginForm_FormClosing;
            label1.Font = new Font(Font.FontFamily.Name, 20);
            passwordChar = textBox2.PasswordChar;

            _ctx = new Context();
        }

        /// <summary>
        /// Обработчик события нажатия клавиш на текстовом поле,
        /// который при нажатии "Enter" производит попытку авторизации.
        /// </summary>
        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                button1_Click(null,EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик события закрытия формы.
        /// </summary>
        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall)
                return;

            var result = MessageBox.Show("Приложение будет закрыто! Продолжить?", "Закрытие приложения",
                MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.No)
                e.Cancel = true;
        }

        /// <summary>
        /// Обработчик события нажатия на кнопку авторизации
        /// </summary>
        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Заполните все поля для прохождения авторизации!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Form frm = null;
            try
            {
                if (!await _ctx.Login(textBox1.Text, textBox2.Text))
                {
                    MessageBox.Show("Неверный логин и/или пароль", "Неверные данные", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }


                switch (Context.CurrentUser.RightsLevel.Name)
                {
                    case "Директор":
                        frm = new DirectorForm();
                        break;
                    case "Системный администратор":
                        frm = new SysAdminForm();
                        break;
                    case "Сотрудник":
                        frm = new EmployeeForm();
                        break;
                    default:
                        throw new Exception();
                }

                textBox1.Text = textBox2.Text = string.Empty;

                frm.Owner = this;
                frm.Show();
                Hide();
            }
            catch
            {
                frm?.Dispose();
                this.Show();
                MessageBox.Show("Произошла ошибка во время авторизации!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события перемещения курсора мыши на метке "Пароль",
        /// который отображает пароль
        /// </summary>
        private void label3_MouseHover(object sender, EventArgs e)
        {
            textBox2.PasswordChar = '\0';
        }

        /// <summary>
        /// Обработчик события выхода курсора мыши на метке "Пароль",
        /// который скрывает пароль
        /// </summary>
        private void label3_MouseLeave(object sender, EventArgs e)
        {
            textBox2.PasswordChar = passwordChar;
        }
    }
}
