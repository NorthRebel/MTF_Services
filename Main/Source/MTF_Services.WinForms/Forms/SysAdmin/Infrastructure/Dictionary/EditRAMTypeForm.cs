using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Dictionary
{
    /// <summary>
    /// Класс формы редактирования типов оперативной памяти
    /// </summary>
    public partial class EditRAMTypeForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private Context _ctx;

        /// <summary>
        /// Список типов оперативной памяти
        /// </summary>
        public BindingList<RamType> RamTypes { get; set; }

        /// <summary>
        /// Текущий тип оперативной памяти
        /// </summary>
        public RamType CurrentRamType { get; set; }

        /// <summary>
        /// Наименование типа оперативной памяти до изменения
        /// </summary>
        private string _ramTypeNameBeforeEditing;

        /// <summary>
        /// Режим работы формы.
        /// </summary>
        private FormMode _formMode;

        /// <summary>
        /// Флаг редактирования типов оперативной памяти
        /// </summary>
        public bool Edited { get; set; }

        /// <summary>
        /// Конструктор формы редактирования типов оперативной памяти
        /// </summary>
        public EditRAMTypeForm()
        {
            InitializeComponent();
            _formMode = FormMode.None;
            _ctx = new Context();
            EnDisFields(false);
            BindCollection();
        }

        /// <summary>
        /// Конструктор для выбора типа оперативной памяти из списка
        /// </summary>
        /// <param name="findMode">Флаг поиска типа оперативной памяти</param>
        public EditRAMTypeForm(bool findMode) : this()
        {
            if (!findMode)
                throw new ArgumentException("Неверное значение флага!");
            _formMode = FormMode.None;
            but_Select.Visible = findMode;
            but_Select.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
        }

        /// <summary>
        /// Привязка коллекции типов оперативной памяти
        /// </summary>
        private void BindCollection()
        {
            RamTypes = _ctx.GetRAMTypesBS();
            ramTypeBindingSource.DataSource = RamTypes;
            listBox1.DataSource = ramTypeBindingSource;
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
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который осуществляет добавление нового типа оперативной памяти
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            RamTypes.AddNew();
            ramTypeBindingSource.MoveLast();
            EnDisFields(true);
            _formMode = FormMode.Add;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который осуществляет редактирование выбранного типа оперативной памяти
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (CurrentRamType != null)
            {
                EnDisFields(true);
                _ramTypeNameBeforeEditing = CurrentRamType.Name;
                _formMode = FormMode.Edit;
            }
            else
                MessageBox.Show("Выберите тип оперативной памяти из списка или добавьте новый!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных типов оперативной памяти
        /// </summary>
        private void ramTypeBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as RamType;
            if (selectedItem != null)
                CurrentRamType = selectedItem;
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
                    if (string.IsNullOrWhiteSpace(CurrentRamType.Name))
                    {
                        MessageBox.Show("Введите наименование типа оперативной памяти!", "Предупреждение", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }

                    bool existed = false;

                    switch (_formMode)
                    {
                        case FormMode.Add:
                            existed = await _ctx.CheckRAMTypeForDublicate(CurrentRamType.Name);
                            break;
                        case FormMode.Edit:
                            existed = await _ctx.CheckRAMTypeForDublicate(CurrentRamType.Name) && !CurrentRamType.Name.Equals(_ramTypeNameBeforeEditing);
                            break;
                    }

                    if (existed)
                    {
                        MessageBox.Show("Тип оперативной памяти с таким наименованием уже существует!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (_formMode == FormMode.Add)
                        RamTypes.EndNew(RamTypes.IndexOf(CurrentRamType));
                    await _ctx.SaveChangesAsync();
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
                            int indexOfElement = RamTypes.IndexOf(CurrentRamType);
                            CurrentRamType = _ctx.CancelChanges(CurrentRamType);
                            RamTypes[indexOfElement] = CurrentRamType;
                        }
                        else
                            RamTypes.CancelNew(RamTypes.IndexOf(CurrentRamType));
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
        /// который совершает подтверждение выбранного типа оперативной памяти
        /// </summary>
        private void but_Select_Click(object sender, EventArgs e)
        {
            if (CurrentRamType != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите тип оперативной памяти из списка или добавьте новый!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditRAMTypeForm_FormClosing(object sender, FormClosingEventArgs e)
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
        /// Ограничение ввода в наименовании добавляемого/редактируемого типа оперативной памяти
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
        /// который производит поиск типа оперативной памяти по наименованию
        /// </summary>
        private void picBtn_FindByCondition_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_ConditionToFind.Text))
            {
                MessageBox.Show("Введите условие для поиска типа оперативной памяти!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            var firstFoundedElement = RamTypes.FirstOrDefault(x => x.Name.ToUpper().Contains(textBox_ConditionToFind.Text.ToUpper()));
            if (firstFoundedElement != null)
                ramTypeBindingSource.Position = RamTypes.IndexOf(firstFoundedElement);
            else
                MessageBox.Show("Не удалось найти тип оперативной памяти по указанному условию!", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
