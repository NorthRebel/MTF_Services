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
    /// Класс формы редактирования интерфейсов накопителей
    /// </summary>
    public partial class EditStorageInterfaceForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Список интерфейсов накопителей
        /// </summary>
        public BindingList<StrorageInterface> StorageInterfaces { get; set; }

        /// <summary>
        /// Текущий интерфейс накопителя
        /// </summary>
        public StrorageInterface CurrentStorageInterface { get; set; }

        /// <summary>
        /// Наименование интерфейса накопителя до изменения
        /// </summary>
        private string _storageInterfaceNameBeforeEditing;

        /// <summary>
        /// Режим работы формы.
        /// </summary>
        private FormMode _formMode;

        /// <summary>
        /// Флаг редактирования интерфейсов накопителей
        /// </summary>
        public bool Edited { get; set; }

        /// <summary>
        /// Конструктор формы редактирования интерфейсов накопителей
        /// </summary>
        public EditStorageInterfaceForm()
        {
            InitializeComponent();
            _formMode = FormMode.None;
            _ctx = new Context();
            EnDisFields(false);
            BindCollection();
        }

        /// <summary>
        /// Конструктор для выбора интерфейса накопителя из списка
        /// </summary>
        /// <param name="findMode">Флаг поиска интерфейса накопителя</param>
        public EditStorageInterfaceForm(bool findMode) : this()
        {
            if (!findMode)
                throw new ArgumentException("Неверное значение флага!");
            _formMode = FormMode.None;
            but_Select.Visible = findMode;
            but_Select.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
        }

        /// <summary>
        /// Привязка коллекции интерфейсов накопителей
        /// </summary>
        private void BindCollection()
        {
            StorageInterfaces = _ctx.GetStorageInterfacesBS();
            strorageInterfaceBindingSource.DataSource = StorageInterfaces;
            listBox1.DataSource = strorageInterfaceBindingSource;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который осуществляет добавление нового интерфейса накопителя
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            StorageInterfaces.AddNew();
            strorageInterfaceBindingSource.MoveLast();
            EnDisFields(true);
            _formMode = FormMode.Add;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который осуществляет редактирование выбранного интерфейса накопителя
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (CurrentStorageInterface != null)
            {
                EnDisFields(true);
                _storageInterfaceNameBeforeEditing = CurrentStorageInterface.Name;
                _formMode = FormMode.Edit;
            }
            else
                MessageBox.Show("Выберите интерфейс накопителя из списка или добавьте новый!", "Предупреждение",
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
        /// Обработчик события изменения текущего элемента в источнике данных интерфейсов накопителя
        /// </summary>
        private void strorageInterfaceBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as StrorageInterface;
            if (selectedItem != null)
                CurrentStorageInterface = selectedItem;
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
                    if (string.IsNullOrWhiteSpace(CurrentStorageInterface.Name))
                    {
                        MessageBox.Show("Введите наименование интерфейса накопителя!", "Предупреждение", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }

                    bool existed = false;

                    switch (_formMode)
                    {
                        case FormMode.Add:
                            existed = await _ctx.CheckStorageInterfaceForDublicate(CurrentStorageInterface.Name);
                            break;
                        case FormMode.Edit:
                            existed = await _ctx.CheckStorageInterfaceForDublicate(CurrentStorageInterface.Name) && !CurrentStorageInterface.Name.Equals(_storageInterfaceNameBeforeEditing);
                            break;
                    }

                    if (existed)
                    {
                        MessageBox.Show("Интерфейс накопителя с таким наименованием уже существует!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (_formMode == FormMode.Add)
                        StorageInterfaces.EndNew(StorageInterfaces.IndexOf(CurrentStorageInterface));
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
                            int indexOfElement = StorageInterfaces.IndexOf(CurrentStorageInterface);
                            CurrentStorageInterface = _ctx.CancelChanges(CurrentStorageInterface);
                            StorageInterfaces[indexOfElement] = CurrentStorageInterface;
                        }
                        else
                            StorageInterfaces.CancelNew(StorageInterfaces.IndexOf(CurrentStorageInterface));
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
        /// который совершает подтверждение выбранного интерфейса накопителя
        /// </summary>
        private void but_Select_Click(object sender, EventArgs e)
        {
            if (CurrentStorageInterface != null)
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
        private void EditStorageInterfaceForm_FormClosing(object sender, FormClosingEventArgs e)
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
        /// Ограничение ввода в наименовании добавляемого/редактируемого интерфейса накопителя
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
        /// который производит поиск интерфейса накопителя по наименованию
        /// </summary>
        private void picBtn_FindByCondition_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_ConditionToFind.Text))
            {
                MessageBox.Show("Введите условие для поиска интерфейса накопителя!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            var firstFoundedElement = StorageInterfaces.FirstOrDefault(x => x.Name.ToUpper().Contains(textBox_ConditionToFind.Text.ToUpper()));
            if (firstFoundedElement != null)
                strorageInterfaceBindingSource.Position = StorageInterfaces.IndexOf(firstFoundedElement);
            else
                MessageBox.Show("Не удалось найти интерфейс накопителя по указанному условию!", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
