using System;
using System.Drawing;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Users
{
    /// <summary>
    /// Класс формы редактирования пользователей
    /// </summary>
    public partial class EditUsersForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Режим работы формы.
        /// </summary>
        private FormMode _formMode;

        /// <summary>
        /// Логин пользователя перед изменением
        /// </summary>
        private string _loginBeforeEdit;

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновке в источнике данных должностей
        /// </summary>
        private bool _afterSuspend;

        private bool _isDirectorRegistered;

        /// <summary>
        /// Экземпляр текущего пользователя
        /// </summary>
        public User CurrentUser { get; set; }

        /// <summary>
        /// Конструктор формы регистрации нового польователя
        /// </summary>
        public EditUsersForm()
        {
            InitializeComponent();
            btn_Cancel.Image = new Bitmap(Resources.no, 20, 20);
            btn_Save.Image = new Bitmap(Resources.camera_test, 20, 20);

            _ctx = new Context();
            InitBindings();
            CurrentUser = new User();
            userBindingSource.DataSource = CurrentUser;
            _formMode = FormMode.Add;
            pictureBox1_Click(null, EventArgs.Empty);

            try
            {
                _isDirectorRegistered = _ctx.IsDirReg();
            }
            catch
            {
                
            }
        }


        /// <summary>
        /// Конструктор формы редактирования учетных данных пользователя
        /// </summary>
        /// <param name="selectedUser">Выбранный пользователь</param>
        public EditUsersForm(User selectedUser) : this()
        {
            CurrentUser = selectedUser;
            userBindingSource.DataSource = CurrentUser;
            _loginBeforeEdit = CurrentUser.Login;
            userBindingSource.ResumeBinding();
            Text = "Редактирование учетных данных пользователя";
            _formMode = FormMode.Edit;
        }

        /// <summary>
        /// Инициализация привязок
        /// </summary>
        private void InitBindings()
        {
            rightsLevelBindingSource.DataSource = _ctx.GetRightsLevels();
            BindPositions();
        }

        /// <summary>
        /// Привязка должностей сотрудника
        /// </summary>
        private void BindPositions()
        {
            positionBindingSource.DataSource = _ctx.GetPositions();
            comboBox2.DataSource = positionBindingSource;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно редактирования должностей сотрудников
        /// </summary>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var editUsrPositionForm = new EditUsrPositionForm();
            if (editUsrPositionForm.ShowDialog() == DialogResult.OK)
            {
                var selectedPosition = positionBindingSource.Current as Position;
                BindPositions();
                if (selectedPosition != null)
                {
                    int pos = positionBindingSource.IndexOf(selectedPosition);
                    if (pos > -1)
                        positionBindingSource.Position = pos;
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно поиска должности сотрудника
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var editUsrPositionForm = new EditUsrPositionForm(true);
            if (editUsrPositionForm.ShowDialog() == DialogResult.OK)
            {
                if (editUsrPositionForm.Edited)
                    BindPositions();

                positionBindingSource_CurrentChanged(positionBindingSource, EventArgs.Empty);
                positionBindingSource.Position = positionBindingSource.IndexOf(editUsrPositionForm.CurrentPosition);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит отмену последнего добавления/редактирования, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который выполняет сохранение изменений
        /// </summary>
        private async void btn_Save_Click(object sender, EventArgs e)
        {
            if (_formMode != FormMode.None)
            {
                try
                {
                    var selectedRightsLevel = rightsLevelBindingSource.Position > -1 ? rightsLevelBindingSource.Current as RightsLevel : null;
                    var selectedPosition = positionBindingSource.Position > -1 ? positionBindingSource.Current as Position : null;

                    if (string.IsNullOrWhiteSpace(CurrentUser.Login) || string.IsNullOrWhiteSpace(CurrentUser.Password)
                        || string.IsNullOrWhiteSpace(txt_ConfirmPass.Text) || selectedRightsLevel == null || selectedPosition == null)
                    {
                        MessageBox.Show("Введите все поля для сохранения изменений!", "Предупреждение",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (!txt_ConfirmPass.Text.Equals(CurrentUser.Password))
                    {
                        MessageBox.Show("Пароли не совпадают!", "Предупреждение",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    bool existed = false;

                    switch (_formMode)
                    {
                        case FormMode.Add:
                            existed = await _ctx.CheckUserLoginForDublicate(CurrentUser.Login);
                            break;
                        case FormMode.Edit:
                            existed = await _ctx.CheckUserLoginForDublicate(CurrentUser.Login) && !CurrentUser.Login.Equals(_loginBeforeEdit);
                            break;
                    }

                    if (existed)
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (_isDirectorRegistered)
                    {
                        MessageBox.Show("Генеральный директор предприятия уже зарегистрирован!");
                        return;
                    }

                    switch (_formMode)
                    {
                        case FormMode.Add:
                            await _ctx.AddNewUser(CurrentUser);
                            break;
                        case FormMode.Edit:
                            await _ctx.EditUser(CurrentUser);
                            break;
                    }

                    _formMode = FormMode.None;
                    DialogResult = DialogResult.OK;
                    MessageBox.Show("Изменения успешно сохранены!", "Информация", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Изменения не удалось сохранить!", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditUsersForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_formMode != FormMode.None)
            {
                var result = MessageBox.Show("Изменения не будут сохранены! Продолжить?", "Отмена изменений",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }
 
        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных уровней привилегий
        /// </summary>
        private void rightsLevelBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                rightsLevelBindingSource.SuspendBinding();
                if (CurrentUser != null)
                    CurrentUser.RightLevelId = 0;
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных должностей
        /// </summary>
        private void positionBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (positionBindingSource.IsBindingSuspended)
            {
                _afterSuspend = true;
                positionBindingSource.ResumeBinding();
            }
            else if (_afterSuspend)
            {
                _afterSuspend = false;
                positionBindingSource.Position = comboBox2.SelectedIndex;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит отмену выбранной должности
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            positionBindingSource.SuspendBinding();
            if (CurrentUser != null)
                CurrentUser.PositionId = 0;
            textBox4.Text = string.Empty;
            textBox5.Text = string.Empty;
        }

        /// <summary>
        /// Обработчик события изменения текущего индекса в списке должностей
        /// </summary>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > -1)
                positionBindingSource_CurrentChanged(positionBindingSource, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши в текстовом поле с Логином,
        /// который ограничивает ввод только латиницы, цифр, символа нижнего подчеркивания и управляющих символов
        /// </summary>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var l = e.KeyChar;
            if (!
                ((l >= 'A' && l <= 'z')
                 || (l >= '0' && l <= '9')
                 || l == '_'
                 || l == '\b'
                 || char.IsControl(l)))
                e.Handled = true;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши в текстовом поле с ФИО,
        /// который ограничивает ввод только русских букв, пробела и управляющих символов
        /// </summary>
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            var l = e.KeyChar;
            if (!
                ((l >= 'А' && l <= 'я')
                 || l == (int)Keys.Space
                 || l == '\b'
                 || char.IsControl(l)))
                e.Handled = true;
        }

        /// <summary>
        /// Обработчик события нажатий клавиш клавиатуры на форме
        /// </summary>
        private void EditUsersForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Enter:
                    btn_Save_Click(null, EventArgs.Empty);
                    break;
            }
        }
    }
}
