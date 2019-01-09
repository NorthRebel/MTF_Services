using System;
using System.Drawing;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;
using MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Dictionary;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Parts
{
    /// <summary>
    /// Класс формы редактирования накопителя
    /// </summary>
    public partial class EditStorageForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Редактируемый накопитель
        /// </summary>
        public Strorage CurrentStrorage { get; set; }

        /// <summary>
        /// Наименование модели накопителя перед редактированием
        /// </summary>
        private string _storageModelBeforeEdit;

        /// <summary>
        /// Наименование производителя накопителя перед редактированием
        /// </summary>
        private string _storageManufacturerBeforeEdit;

        /// <summary>
        /// Режим работы формы.
        /// </summary>
        private FormMode _formMode;

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновке в источнике данных производителей
        /// </summary>
        private bool _afterManufacturerSuspend;

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновке в источнике данных типов интерфейса
        /// </summary>
        private bool _afterStorageInterfaceSuspend;

        /// <summary>
        /// Конструктор формы создания нового накопителя
        /// </summary>
        public EditStorageForm()
        {
            InitializeComponent();
            btn_Cancel.Image = new Bitmap(Resources.no, 20, 20);
            btn_Save.Image = new Bitmap(Resources.camera_test, 20, 20);

            _ctx = new Context();

            BindAll();

            CurrentStrorage = new Strorage();
            strorageBindingSource.DataSource = CurrentStrorage;

            _formMode = FormMode.Add;
            Text = "Создание накопителя";
        }

        /// <summary>
        /// Конструктор формы редактирования выбранного накопителя
        /// </summary>
        public EditStorageForm(Strorage selectedStorage) : this()
        {
            _formMode = FormMode.Edit;
            Text = "Редактирование накопителя";

            InitEditStorage(selectedStorage);
        }

        #region Bindings

        /// <summary>
        /// Инициализация привязок всех коллекций
        /// </summary>
        private void BindAll()
        {
            BindManufacturer();
            BindStorageInterfaces();

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
        /// Инициализация привязки коллекции с интерфейсами обмена данных
        /// </summary>
        private void BindStorageInterfaces()
        {
            strorageInterfaceBindingSource.DataSource = _ctx.GetStorageInterfaces();
            comboBox2.DataSource = strorageInterfaceBindingSource;
        }

        /// <summary>
        /// Инициализация привязок для редактирования выбранного накопителя
        /// </summary>
        private void InitEditStorage(Strorage selectedStrorage)
        {
            CurrentStrorage = selectedStrorage;
            strorageBindingSource.DataSource = CurrentStrorage;

            _storageManufacturerBeforeEdit = CurrentStrorage.Manufacturer.Name;
            _storageModelBeforeEdit = CurrentStrorage.Model;

            strorageBindingSource.ResumeBinding();
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
        /// Обработчик события изменения текущего элемента в источнике данных типа интерфейса накопителя
        /// </summary>
        private void strorageInterfaceBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (strorageInterfaceBindingSource.IsBindingSuspended)
            {
                _afterStorageInterfaceSuspend = true;
                strorageInterfaceBindingSource.ResumeBinding();
            }
            else if (_afterStorageInterfaceSuspend)
            {
                _afterStorageInterfaceSuspend = false;
                strorageInterfaceBindingSource.Position = comboBox2.SelectedIndex;
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
            if (CurrentStrorage != null)
                CurrentStrorage.ManufacturerId = 0;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление выбранного интерфейса
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            strorageInterfaceBindingSource.SuspendBinding();
            if (CurrentStrorage != null)
                CurrentStrorage.StrorageInterfaceId = 0;
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditStorageForm_FormClosing(object sender, FormClosingEventArgs e)
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
        /// который производит отмену создания / редактирования накопителя, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит сохранение добавляемой / редактируемой записи об накопителе
        /// </summary>
        private async void btn_Save_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || string.IsNullOrWhiteSpace(CurrentStrorage.Model) ||
                CurrentStrorage.Price <= 0 || CurrentStrorage.Volume <= 0)
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
                        existed = await _ctx.CheckStorageForDublicate(CurrentStrorage.Model, selectedManufacturer.Name);
                        break;
                    case FormMode.Edit:
                        existed = await _ctx.CheckStorageForDublicate(CurrentStrorage.Model, selectedManufacturer.Name)
                                  && (!CurrentStrorage.Model.Equals(_storageModelBeforeEdit) || !selectedManufacturer.Name.Equals(_storageManufacturerBeforeEdit));
                        break;
                }

                if (existed)
                {
                    MessageBox.Show("Накопитель с таким производителем и моделью уже существует!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                switch (_formMode)
                {
                    case FormMode.Add:
                        await _ctx.AddNewStorage(CurrentStrorage);
                        MessageBox.Show("Новый накопитель успешно сохранен!", "Информация", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        break;
                    case FormMode.Edit:
                        await _ctx.EditStorage(CurrentStrorage);
                        MessageBox.Show("Изменения в накопителе успешно сохранены!", "Информация", MessageBoxButtons.OK,
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
        /// который открывает диалоговое окно поиска интерфейса накопителя
        /// </summary>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var editStorageInterface = new EditStorageInterfaceForm(true);
            if (editStorageInterface.ShowDialog() == DialogResult.OK)
            {
                if (editStorageInterface.Edited)
                    BindStorageInterfaces();

                strorageInterfaceBindingSource_CurrentChanged(strorageInterfaceBindingSource, EventArgs.Empty);
                strorageInterfaceBindingSource.Position =
                    strorageInterfaceBindingSource.IndexOf(editStorageInterface.CurrentStorageInterface);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно редактирования интерфейсов накопителя
        /// </summary>
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            var editStorageInterface = new EditStorageInterfaceForm();
            if (editStorageInterface.ShowDialog() == DialogResult.OK)
            {
                var strorageInterface = strorageInterfaceBindingSource.Current as StrorageInterface;
                BindStorageInterfaces();
                if (strorageInterface != null)
                {
                    int pos = strorageInterfaceBindingSource.IndexOf(strorageInterface);
                    if (pos > -1)
                        strorageInterfaceBindingSource.Position = pos;
                }
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
        /// Обработчик события изменения текущего индекса в списке интерфейсов накопителя
        /// </summary>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > -1)
                strorageInterfaceBindingSource_CurrentChanged(strorageInterfaceBindingSource, EventArgs.Empty);
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
        /// Обработчик события нажатия клавиши в текстовом поле с объемом,
        /// который ограничивает ввод только цифр и управляющих символов
        /// </summary>
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
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
