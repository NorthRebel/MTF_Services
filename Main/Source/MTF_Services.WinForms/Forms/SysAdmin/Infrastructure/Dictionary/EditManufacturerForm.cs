using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;
using Resources = MTF_Services.WinForms.Properties.Resources;

namespace MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Dictionary
{
    /// <summary>
    /// Класс формы редактирования производителей
    /// </summary>
    public partial class EditManufacturerForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Список производителей
        /// </summary>
        public BindingList<Manufacturer> Manufacturers { get; set; }

        /// <summary>
        /// Текущий производитель
        /// </summary>
        public Manufacturer CurrentManufacturer { get; set; }

        /// <summary>
        /// Наименование производителя до изменения
        /// </summary>
        private string _manufacturerNameBeforeEditing;

        /// <summary>
        /// Режим работы формы.
        /// </summary>
        private FormMode _formMode;

        /// <summary>
        /// Флаг редактирования производителей
        /// </summary>
        public bool Edited { get; set; }

        /// <summary>
        /// Конструктор формы редактирования производителей
        /// </summary>
        public EditManufacturerForm()
        {
            InitializeComponent();
            _formMode = FormMode.None;
            _ctx = new Context();
            EnDisFields(false);
            BindCollection();
        }

        /// <summary>
        /// Конструктор для выбора производителя из списка
        /// </summary>
        /// <param name="findMode">Флаг поиска производителя</param>
        public EditManufacturerForm(bool findMode) : this()
        {
            if (!findMode)
                throw new ArgumentException("Неверное значение флага!");
            _formMode = FormMode.None;
            but_Select.Visible = findMode;
            but_Select.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
        }

        /// <summary>
        /// Привязка коллекции производителей
        /// </summary>
        private void BindCollection()
        {
            Manufacturers = _ctx.GetManufacturers();
            manufacturerBindingSource.DataSource = Manufacturers;
            listBox1.DataSource = manufacturerBindingSource;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который осуществляет добавление нового производителя
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            Manufacturers.AddNew();
            manufacturerBindingSource.MoveLast();
            EnDisFields(true);
            _formMode = FormMode.Add;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который осуществляет редактирование выбранного производителя
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (CurrentManufacturer != null)
            {
                EnDisFields(true);
                _manufacturerNameBeforeEditing = CurrentManufacturer.Name;
                _formMode = FormMode.Edit;
            }
            else
                MessageBox.Show("Выберите производителя из списка или добавьте нового!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// Обработчик события изменения текущего элемента в источнике данных производителей
        /// </summary>
        private void manufacturerBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as Manufacturer;
            if (selectedItem != null)
                CurrentManufacturer = selectedItem;
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
                    if (string.IsNullOrWhiteSpace(CurrentManufacturer.Name))
                    {
                        MessageBox.Show("Введите наименование производителя!", "Предупреждение", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }

                    bool existed = false;

                    switch (_formMode)
                    {
                        case FormMode.Add:
                            existed = await _ctx.CheckManufacturerForDublicate(CurrentManufacturer.Name);
                            break;
                        case FormMode.Edit:
                            existed = await _ctx.CheckManufacturerForDublicate(CurrentManufacturer.Name) && !CurrentManufacturer.Name.Equals(_manufacturerNameBeforeEditing);
                            break;
                    }

                    if (existed)
                    {
                        MessageBox.Show("Производитель с таким наименованием уже существует!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (_formMode == FormMode.Add)
                        Manufacturers.EndNew(Manufacturers.IndexOf(CurrentManufacturer));
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
                            int indexOfElement = Manufacturers.IndexOf(CurrentManufacturer);
                            CurrentManufacturer = _ctx.CancelChanges(CurrentManufacturer);
                            Manufacturers[indexOfElement] = CurrentManufacturer;
                        }
                        else 
                            Manufacturers.CancelNew(Manufacturers.IndexOf(CurrentManufacturer));
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
        /// который совершает подтверждение выбранного производителя
        /// </summary>
        private void but_Select_Click(object sender, EventArgs e)
        {
            if (CurrentManufacturer != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите элемент из списка или добавьте новый!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditManufacturerForm_FormClosing(object sender, FormClosingEventArgs e)
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
        /// Ограничение ввода в наименовании добавляемого/редактируемого производителя
        /// </summary>
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsLetter(e.KeyChar) && e.KeyChar != (int) Keys.Space)
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
        /// который производит поиск производителя по наименованию
        /// </summary>
        private void picBtn_FindByCondition_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_ConditionToFind.Text))
            {
                MessageBox.Show("Введите условие для поиска производителя!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            var firstFoundedElement = Manufacturers.FirstOrDefault(x => x.Name.ToUpper().Contains(textBox_ConditionToFind.Text.ToUpper()));
            if (firstFoundedElement != null)
                manufacturerBindingSource.Position = Manufacturers.IndexOf(firstFoundedElement);
            else
                MessageBox.Show("Не удалось найти производителя по указанному условию!", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
