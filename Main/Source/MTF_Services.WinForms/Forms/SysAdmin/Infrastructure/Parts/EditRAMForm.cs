using System;
using System.Drawing;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;
using MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Dictionary;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Parts
{
    /// <summary>
    /// Класс формы редактирования оперативной памяти
    /// </summary>
    public partial class EditRAMForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Редактируемуя оперативная память
        /// </summary>
        public RAM CurrentRAM { get; set; }

        /// <summary>
        /// Наименование модели оперативной памяти перед редактированием
        /// </summary>
        private string _ramModelBeforeEdit;

        /// <summary>
        /// Наименование производителя оперативной памяти перед редактированием
        /// </summary>
        private string _ramManufacturerBeforeEdit;

        /// <summary>
        /// Режим работы формы.
        /// </summary>
        private FormMode _formMode;

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновке в источнике данных производителей
        /// </summary>
        private bool _afterManufacturerSuspend;

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновке в источнике данных типов оперативной памяти
        /// </summary>
        private bool _afterRamTypeSuspend;

        /// <summary>
        /// Конструктор формы создания новой оперативной памяти
        /// </summary>
        public EditRAMForm()
        {
            InitializeComponent();
            btn_Cancel.Image = new Bitmap(Resources.no, 20, 20);
            btn_Save.Image = new Bitmap(Resources.camera_test, 20, 20);

            _ctx = new Context();

            BindAll();

            CurrentRAM = new RAM();
            rAMBindingSource.DataSource = CurrentRAM;

            _formMode = FormMode.Add;
            Text = "Создание оперативной памяти";
        }

        /// <summary>
        /// Конструктор формы редактирования выбранной оперативной памяти
        /// </summary>
        public EditRAMForm(RAM selectedRAM) : this()
        {
            _formMode = FormMode.Edit;
            Text = "Редактирование оперативной памяти";

            InitEditRAM(selectedRAM);
        }

        #region Bindings

        /// <summary>
        /// Инициализация привязок всех коллекций
        /// </summary>
        private void BindAll()
        {
            BindManufacturer();
            BindRAMTypes();

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        /// <summary>
        /// Инициализация привязки коллекции с производителями
        /// </summary>
        private void BindManufacturer()
        {
            manufacturerBindingSource.DataSource = _ctx.GetManufacturers();
            comboBox1.DataSource = manufacturerBindingSource;
        }

        /// <summary>
        /// Инициализация привязки коллекции с типами оперативной памяти
        /// </summary>
        private void BindRAMTypes()
        {
            ramTypeBindingSource.DataSource = _ctx.GetRamsTypes();
            comboBox2.DataSource = ramTypeBindingSource;
        }

        /// <summary>
        /// Инициализация привязок для редактирования выбранной оперативной памяти
        /// </summary>
        private void InitEditRAM(RAM selectedRAM)
        {
            CurrentRAM = selectedRAM;
            rAMBindingSource.DataSource = CurrentRAM;

            _ramManufacturerBeforeEdit = CurrentRAM.Manufacturer.Name;
            _ramModelBeforeEdit = CurrentRAM.Model;

            rAMBindingSource.ResumeBinding();
        }

        #endregion

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных производителей
        /// </summary>
        private void manufacturerBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (manufacturerBindingSource.IsBindingSuspended)
            {
                _afterManufacturerSuspend = true;
                manufacturerBindingSource.ResumeBinding();
            }
            else if (_afterManufacturerSuspend)
            {
                _afterManufacturerSuspend = false;
                manufacturerBindingSource.Position = comboBox1.SelectedIndex;
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных типов оперативной памяти
        /// </summary>
        private void ramTypeBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (ramTypeBindingSource.IsBindingSuspended)
            {
                _afterRamTypeSuspend = true;
                ramTypeBindingSource.ResumeBinding();
            }
            else if (_afterRamTypeSuspend)
            {
                _afterRamTypeSuspend = false;
                ramTypeBindingSource.Position = comboBox2.SelectedIndex;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление выбранного производителя
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            manufacturerBindingSource.SuspendBinding();
            if (CurrentRAM != null)
                CurrentRAM.ManufacturerId = 0;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление выбранного типа оперативной памяти
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            ramTypeBindingSource.SuspendBinding();
            if (CurrentRAM != null)
                CurrentRAM.RamTypeId = 0;
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditRAMForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_formMode != FormMode.None)
            {
                var result = MessageBox.Show("Изменения не будут сохранены! Продолжить?", "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену создания / редактирования оперативной памяти, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит сохранение добавляемой / редактируемой записи об оперативной памяти
        /// </summary>
        private async void btn_Save_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || string.IsNullOrWhiteSpace(CurrentRAM.Model) ||
                CurrentRAM.Price <= 0 || CurrentRAM.Volume <= 0)
            {
                MessageBox.Show("Заполните все поля для сохранения изменений!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var selectedManufacturer = manufacturerBindingSource.Current as Manufacturer;
                bool existed = false;

                switch (_formMode)
                {
                    case FormMode.Add:
                        existed = await _ctx.CheckRAMForDublicate(CurrentRAM.Model, selectedManufacturer.Name);
                        break;
                    case FormMode.Edit:
                        existed = await _ctx.CheckRAMForDublicate(CurrentRAM.Model, selectedManufacturer.Name)
                                  && (!CurrentRAM.Model.Equals(_ramModelBeforeEdit) || !selectedManufacturer.Name.Equals(_ramManufacturerBeforeEdit));
                        break;
                }

                if (existed)
                {
                    MessageBox.Show("Оперативная память с таким производителем и моделью уже существует!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                switch (_formMode)
                {
                    case FormMode.Add:
                        await _ctx.AddNewRAM(CurrentRAM);
                        MessageBox.Show("Новая оперативная память успешно сохранена!", "Информация", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        break;
                    case FormMode.Edit:
                        await _ctx.EditRAM(CurrentRAM);
                        MessageBox.Show("Изменения в оперативной памяти успешно сохранены!", "Информация", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        break;
                }

                _formMode = FormMode.None;
                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                MessageBox.Show("Изменения не удалось сохранить!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно формы редактирования производителей
        /// </summary>
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            var editManufacturer = new EditManufacturerForm();
            if (editManufacturer.ShowDialog() == DialogResult.OK)
            {
                var selectedManufacturer = manufacturerBindingSource.Current as Manufacturer;
                BindManufacturer();
                if (selectedManufacturer != null)
                {
                    int pos = manufacturerBindingSource.IndexOf(selectedManufacturer);
                    if (pos > -1)
                        manufacturerBindingSource.Position = pos;
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно поиска производителя
        /// </summary>
        private void pictureBox17_Click(object sender, EventArgs e)
        {
            var editManufacturer = new EditManufacturerForm(true);
            if (editManufacturer.ShowDialog() == DialogResult.OK)
            {
                if (editManufacturer.Edited)
                    BindManufacturer();

                manufacturerBindingSource_CurrentChanged(manufacturerBindingSource, EventArgs.Empty);
                manufacturerBindingSource.Position =
                    manufacturerBindingSource.IndexOf(editManufacturer.CurrentManufacturer);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно редактирования типов оперативной памяти
        /// </summary>
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            var editRamType = new EditRAMTypeForm();
            if (editRamType.ShowDialog() == DialogResult.OK)
            {
                var selectedRamType = ramTypeBindingSource.Current as RamType;
                BindRAMTypes();
                if (selectedRamType != null)
                {
                    int pos = ramTypeBindingSource.IndexOf(selectedRamType);
                    if (pos > -1)
                        ramTypeBindingSource.Position = pos;
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно поиска типа оперативной памяти
        /// </summary>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var editRamType = new EditRAMTypeForm(true);
            if (editRamType.ShowDialog() == DialogResult.OK)
            {
                if (editRamType.Edited)
                    BindRAMTypes();

                ramTypeBindingSource_CurrentChanged(ramTypeBindingSource, EventArgs.Empty);
                ramTypeBindingSource.Position =
                    ramTypeBindingSource.IndexOf(editRamType.CurrentRamType);
            }
        }

        /// <summary>
        /// Обработчик события изменения текущего индекса в списке производителей
        /// </summary>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
                manufacturerBindingSource_CurrentChanged(manufacturerBindingSource, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик события изменения текущего индекса в списке типов оперативной памяти
        /// </summary>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > -1)
                ramTypeBindingSource_CurrentChanged(ramTypeBindingSource, EventArgs.Empty);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши в текстовом поле с наименованием,
        /// который ограничивает ввод только русских букв, пробела и управляющих символов
        /// </summary>
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var l = e.KeyChar;
            if (!
                ((l >= 'A' && l <= 'z')
                 || l == (int)Keys.Space
                 || char.IsDigit(e.KeyChar)
                 || l == '\b'
                 || char.IsControl(l)))
                e.Handled = true;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши в numericupdown с объемом модуля,
        /// который ограничивает ввод только цифр и управляющих символов
        /// </summary>
        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши в текстовом поле с ценой,
        /// который ограничивает ввод только цифр, управляющих символов, и 1 запятой
        /// </summary>
        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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
                if (e.KeyChar == (char)Keys.Back)
                    return;
            e.Handled = true;
        }
    }
}
