using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Services.Dictionary
{
    /// <summary>
    /// Класс формы редактирования типов программного обеспечения
    /// </summary>
    public partial class EditSoftwareTypeForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private Context _ctx;

        /// <summary>
        /// Список типов программного обеспечения
        /// </summary>
        public BindingList<SoftType> SoftTypes { get; set; }

        /// <summary>
        /// Текущий тип программного обеспечения
        /// </summary>
        public SoftType CurrentSoftType { get; set; }

        /// <summary>
        /// Наименование типа программного обеспечения до изменения
        /// </summary>
        private string _softwareTypeNameBeforeEditing;

        /// <summary>
        /// Режим работы формы.
        /// </summary>
        private FormMode _formMode;

        /// <summary>
        /// Флаг редактирования типов программного обеспечения
        /// </summary>
        public bool Edited { get; set; }

        /// <summary>
        /// Конструктор формы редактирования типов программного обеспечения
        /// </summary>
        public EditSoftwareTypeForm()
        {
            InitializeComponent();
            _formMode = FormMode.None;
            _ctx = new Context();
            EnDisFields(false);
            BindCollection();
        }

        /// <summary>
        /// Конструктор для выбора типа программного обеспечения из списка
        /// </summary>
        /// <param name="findMode">Флаг поиска типа программного обеспечения</param>
        public EditSoftwareTypeForm(bool findMode) : this()
        {
            if (!findMode)
                throw new ArgumentException("Неверное значение флага!");
            _formMode = FormMode.None;
            but_Select.Visible = findMode;
            but_Select.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
        }

        /// <summary>
        /// Привязка коллекции
        /// </summary>
        private void BindCollection()
        {
            SoftTypes = _ctx.GetSoftTypesBS();
            softTypeBindingSource.DataSource = SoftTypes;
            listBox1.DataSource = softTypeBindingSource;
        }

        /// <summary>
        /// Активация/деактивация полей для редактирования
        /// </summary>
        /// <param name="enabled">Флаг активации полей редактирования</param>
        private void EnDisFields(bool enabled)
        {
            textBox2.Enabled = enabled;
            btn_Save.Enabled = enabled;
            btn_Cancel.Enabled = enabled;

            textBox_ConditionToFind.Enabled = !enabled;
            listBox1.Enabled = !enabled;
            picBtn_FindByCondition.Enabled = !enabled;
            picBtn_ClearCondition.Enabled = !enabled;
            button2.Enabled = !enabled;
            button3.Enabled = !enabled;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных типов программного обеспечения
        /// </summary>
        private void softTypeBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as SoftType;
            if (selectedItem != null)
                CurrentSoftType = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который совершает добавление нового типа программного обеспечения
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            SoftTypes.AddNew();
            softTypeBindingSource.MoveLast();
            EnDisFields(true);
            _formMode = FormMode.Add;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который совершает редактирование выбранного типа программного обеспечения
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (CurrentSoftType != null)
            {
                EnDisFields(true);
                _softwareTypeNameBeforeEditing = CurrentSoftType.Name;
                _formMode = FormMode.Edit;
            }
            else
                MessageBox.Show("Выберите тип программного обеспечения из списка или добавьте новое!", "Предупреждение",
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
                    if (string.IsNullOrWhiteSpace(CurrentSoftType.Name))
                    {
                        MessageBox.Show("Введите наименование типа программного обеспечения!", "Предупреждение", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }

                    bool existed = false;

                    switch (_formMode)
                    {
                        case FormMode.Add:
                            existed = await _ctx.CheckSoftTypeForDublicate(CurrentSoftType.Name);
                            break;
                        case FormMode.Edit:
                            existed = await _ctx.CheckSoftTypeForDublicate(CurrentSoftType.Name) && !CurrentSoftType.Name.Equals(_softwareTypeNameBeforeEditing);
                            break;
                    }

                    if (existed)
                    {
                        MessageBox.Show("Тип программного обеспечения с таким наименованием уже существует!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (_formMode == FormMode.Add)
                        SoftTypes.EndNew(SoftTypes.IndexOf(CurrentSoftType));
                    _ctx.SaveChanges();
                    EnDisFields(false);
                    Edited = true;
                    _formMode = FormMode.None;
                    DialogResult = DialogResult.OK;
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
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену последнего добавления/редактирования
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
                            int indexOfElement = SoftTypes.IndexOf(CurrentSoftType);
                            CurrentSoftType = _ctx.CancelChanges(CurrentSoftType);
                            SoftTypes[indexOfElement] = CurrentSoftType;
                        }
                        else
                            SoftTypes.CancelNew(SoftTypes.IndexOf(CurrentSoftType));
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
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который совершает подтверждение выбранного программного обеспечения
        /// </summary>
        private void but_Select_Click(object sender, EventArgs e)
        {
            if (CurrentSoftType != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите тип программного обеспечения из списка или добавьте новый!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditSoftwareTypeForm_FormClosing(object sender, FormClosingEventArgs e)
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
        /// Ограничение ввода в наименовании добавляемого/редактируемого программного обеспечения
        /// </summary>
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsLetter(e.KeyChar) && e.KeyChar != (int)Keys.Space && !Char.IsDigit(e.KeyChar))
                e.Handled = true;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит очистку строки для поиска
        /// </summary>
        private void picBtn_ClearCondition_Click(object sender, EventArgs e)
        {
            textBox_ConditionToFind.Text = string.Empty;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит поиск программного обеспечения по наименованию
        /// </summary>
        private void picBtn_FindByCondition_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_ConditionToFind.Text))
            {
                MessageBox.Show("Введите условие для поиска программного обеспечения!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            var firstFoundedElement =SoftTypes.FirstOrDefault(x => x.Name.ToUpper().Contains(textBox_ConditionToFind.Text.ToUpper()));
            if (firstFoundedElement != null)
                softTypeBindingSource.Position = SoftTypes.IndexOf(firstFoundedElement);
            else
                MessageBox.Show("Не удалось найти программное обеспечение по указанному условию!", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обработчик события нажатий клавиш клавиатуры на форме
        /// </summary>
        private void EditSoftwareTypeForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    btn_Cancel_Click(null,EventArgs.Empty);
                    break;
                case Keys.Enter:
                    btn_Save_Click(null, EventArgs.Empty);
                    break;
            }
        }
    }
}
