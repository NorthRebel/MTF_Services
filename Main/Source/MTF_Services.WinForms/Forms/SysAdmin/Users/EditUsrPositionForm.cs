using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Users
{
    /// <summary>
    /// Класс формы редактирования должностей сотрудников
    /// </summary>
    public partial class EditUsrPositionForm : Form
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
        /// Список должностей
        /// </summary>
        public BindingList<Position> Positions { get; set; }

        /// <summary>
        /// Текущая должность
        /// </summary>
        public Position CurrentPosition { get; set; }

        /// <summary>
        /// Наименование должности до изменения
        /// </summary>
        private string _positionNameBeforeEditing;

        /// <summary>
        /// Флаг редактирования должностей
        /// </summary>
        public bool Edited { get; set; }

        /// <summary>
        /// Конструктор формы редактирования должностей сотрудников
        /// </summary>
        public EditUsrPositionForm()
        {
            InitializeComponent();
            EnDisFields(false);
            _ctx = new Context();
            BindCollection();
            _formMode = FormMode.None;
        }

        /// <summary>
        /// Конструктор формы для выбора должности из списка
        /// </summary>
        /// <param name="findMode"></param>
        public EditUsrPositionForm(bool findMode) : this()
        {
            if (!findMode)
                throw new ArgumentException("Неверное значение флага!");
            but_Select.Visible = findMode;
            _formMode = FormMode.None;
            but_Select.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
        }

        /// <summary>
        /// Привязка коллекции
        /// </summary>
        private void BindCollection()
        {
            Positions = _ctx.GetPositions();
            positionBindingSource.DataSource = Positions;
            dataGridView1.DataSource = positionBindingSource;
        }

        /// <summary>
        /// Активация/деактивация полей для редактирования
        /// </summary>
        /// <param name="enabled">Флаг активации полей редактирования</param>
        private void EnDisFields(bool enabled)
        {
            textBox1.Enabled = enabled;
            textBox2.Enabled = enabled;
            textBox3.Enabled = enabled;
            btn_Cancel.Enabled = enabled;
            btn_Save.Enabled = enabled;

            dataGridView1.Enabled = !enabled;
            button2.Enabled = !enabled;
            button3.Enabled = !enabled;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит добавление новой должности
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            Positions.AddNew();
            positionBindingSource.MoveLast();
            EnDisFields(true);
            _formMode = FormMode.Add;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит редактирование выбранной должности
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (CurrentPosition != null)
            {
                EnDisFields(true);
                _positionNameBeforeEditing = CurrentPosition.Name;
                _formMode = FormMode.Edit;
            }
            else
                MessageBox.Show("Выберите должность из списка или добавьте новую!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    if (string.IsNullOrWhiteSpace(CurrentPosition.Name) || CurrentPosition.AvgSalary <= 0
                        || CurrentPosition.WorkHours <= 0)
                    {
                        MessageBox.Show("Заполните все поля для сохранения изменений!", "Предупреждение", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }

                    bool existed = false;

                    switch (_formMode)
                    {
                        case FormMode.Add:
                            existed = await _ctx.CheckPositionForDublicate(CurrentPosition.Name);
                            break;
                        case FormMode.Edit:
                            existed = await _ctx.CheckPositionForDublicate(CurrentPosition.Name) && !CurrentPosition.Name.Equals(_positionNameBeforeEditing);
                            break;
                    }

                    if (existed)
                    {
                        MessageBox.Show("Должность с таким наименованием уже существует!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (_formMode == FormMode.Add)
                        Positions.EndNew(Positions.IndexOf(CurrentPosition));
                    await _ctx.SaveChangesAsync();
                    EnDisFields(false);
                    _formMode = FormMode.None;
                    Edited = true;
                    MessageBox.Show("Изменения успешно сохранены!", "Информация", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
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
        private void EditUsrPositionForm_FormClosing(object sender, FormClosingEventArgs e)
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
        /// Обработчик события изменения текущего элемента в источнике данных должностей
        /// </summary>
        private void positionBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as Position;
            if (selectedItem != null)
                CurrentPosition = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который подтверждает выбранный элемент из списка должностей
        /// </summary>
        private void but_Select_Click(object sender, EventArgs e)
        {
            if (CurrentPosition != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите должность из списка!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши в текстовом поле с наименованием должности,
        /// который ограничивает ввод только русских букв, дефиса, пробела и управляющих символов
        /// </summary>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var l = e.KeyChar;
            if (!
                ((l >= 'А' && l <= 'я')
                 || l == (int)Keys.Space
                 || l == '-'
                 || l == '\b'
                 || char.IsControl(l)))
                e.Handled = true;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши в текстовом поле с кол-вом рабочих часов,
        /// который ограничивает ввод только цифр и управляющих символов
        /// </summary>
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            var l = e.KeyChar;
            if (!(char.IsDigit(l) || char.IsControl(l) || l == '\b'))
                e.Handled = true;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши в текстовом поле с размером средней зарплаты,
        /// который ограничивает ввод только цифр, управляющих символов, и 1 запятой
        /// </summary>
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txtBox = (TextBox)sender;

            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                if (txtBox.Text.Length == 10 && txtBox.Text.Length <= 10)
                    e.Handled = true;
                return;
            }
            // Точку заменим запятой
            if (e.KeyChar == '.')
                e.KeyChar = ',';

            if (e.KeyChar == ',')
            {
                // Не более одной запятой и запятая не может быть первым символом.
                if ((txtBox.Text.IndexOf(',') != -1) || (txtBox.Text.Length == 0))
                    e.Handled = true;
                return;
            }

            if (Char.IsControl(e.KeyChar))
                if (e.KeyChar == (char) Keys.Back)
                    return;
            e.Handled = true;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену последнего добавления/редактирования, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            if (_formMode != FormMode.None)
            {
                var result = MessageBox.Show("Изменения не будут сохранены! Продолжить?", "Отмена изменений",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        if (_formMode == FormMode.Edit)
                        {
                            int indexOfElement = Positions.IndexOf(CurrentPosition);
                            CurrentPosition = _ctx.CancelChanges(CurrentPosition);
                            Positions[indexOfElement] = CurrentPosition;
                        }
                        else
                            Positions.CancelNew(Positions.IndexOf(CurrentPosition));
                        EnDisFields(false);
                        _formMode = FormMode.None;
                    }
                    catch
                    {
                        MessageBox.Show("Произошла ошибка при отмене изменений!", "Ошибка", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатий клавиш клавиатуры  на форме
        /// </summary>
        private void EditUsrPositionForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}
