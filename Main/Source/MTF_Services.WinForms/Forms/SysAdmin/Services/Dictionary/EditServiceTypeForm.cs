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
    /// Класс формы редактирования типов сервиса
    /// </summary>
    public partial class EditServiceTypeForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Список типов сервиса
        /// </summary>
        public BindingList<ServiceType> ServiceTypes { get; set; }

        /// <summary>
        /// Текущий тип сервиса
        /// </summary>
        public ServiceType CurrentServiceType { get; set; }

        /// <summary>
        /// Наименование типа сервиса до изменения
        /// </summary>
        private string _serviceTypeNameBeforeEditing;

        /// <summary>
        /// Режим работы формы.
        /// </summary>
        private FormMode _formMode;

        /// <summary>
        /// Флаг редактирования типов сервиса
        /// </summary>
        public bool Edited { get; set; }

        /// <summary>
        /// Конструктор формы редактирования типов сервиса
        /// </summary>
        public EditServiceTypeForm()
        {
            InitializeComponent();
            _formMode = FormMode.None;
            _ctx = new Context();
            EnDisFields(false);
            BindCollection();
        }

        /// <summary>
        /// Конструктор для выбора типа сервиса из списка
        /// </summary>
        /// <param name="findMode">Флаг поиска типа сервиса</param>
        public EditServiceTypeForm(bool findMode) : this()
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
            ServiceTypes = _ctx.GetServiceTypesBS();
            serviceTypeBindingSource.DataSource = ServiceTypes;
            listBox1.DataSource = serviceTypeBindingSource;
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
        /// Обработчик события изменения текущего элемента в источнике данных типов сервиса
        /// </summary>
        private void serviceTypeBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as ServiceType;
            if (selectedItem != null)
                CurrentServiceType = selectedItem;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который совершает добавление нового типа сервиса
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            ServiceTypes.AddNew();
            serviceTypeBindingSource.MoveLast();
            EnDisFields(true);
            _formMode = FormMode.Add;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который совершает редактирование выбранного типа сервиса
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (CurrentServiceType != null)
            {
                EnDisFields(true);
                _serviceTypeNameBeforeEditing = CurrentServiceType.Name;
                _formMode = FormMode.Edit;
            }
            else
                MessageBox.Show("Выберите тип сервиса из списка или добавьте новый!", "Предупреждение",
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
                    if (string.IsNullOrWhiteSpace(CurrentServiceType.Name))
                    {
                        MessageBox.Show("Введите наименование типа сервиса!", "Предупреждение", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }

                    bool existed = false;

                    switch (_formMode)
                    {
                        case FormMode.Add:
                            existed = await _ctx.CheckServiceTypeForDublicate(CurrentServiceType.Name);
                            break;
                        case FormMode.Edit:
                            existed = await _ctx.CheckServiceTypeForDublicate(CurrentServiceType.Name) && !CurrentServiceType.Name.Equals(_serviceTypeNameBeforeEditing);
                            break;
                    }

                    if (existed)
                    {
                        MessageBox.Show("Тип сервиса с таким наименованием уже существует!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (_formMode == FormMode.Add)
                        ServiceTypes.EndNew(ServiceTypes.IndexOf(CurrentServiceType));
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
                            int indexOfElement = ServiceTypes.IndexOf(CurrentServiceType);
                            CurrentServiceType = _ctx.CancelChanges(CurrentServiceType);
                            ServiceTypes[indexOfElement] = CurrentServiceType;
                        }
                        else
                            ServiceTypes.CancelNew(ServiceTypes.IndexOf(CurrentServiceType));
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
        /// который совершает подтверждение выбранного типа сервиса
        /// </summary>
        private void but_Select_Click(object sender, EventArgs e)
        {
            if (CurrentServiceType != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите тип сервиса из списка или добавьте новый!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditServiceTypeForm_FormClosing(object sender, FormClosingEventArgs e)
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
        /// Ограничение ввода в наименовании добавляемого/редактируемого типа сервиса
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
        /// который производит поиск типа сервиса по наименованию
        /// </summary>
        private void picBtn_FindByCondition_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_ConditionToFind.Text))
            {
                MessageBox.Show("Введите условие для поиска типа сервиса!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            var firstFoundedElement = ServiceTypes.FirstOrDefault(x => x.Name.ToUpper().Contains(textBox_ConditionToFind.Text.ToUpper()));
            if (firstFoundedElement != null)
                serviceTypeBindingSource.Position = ServiceTypes.IndexOf(firstFoundedElement);
            else
                MessageBox.Show("Не удалось найти тип сервиса по указанному условию!", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обработчик события нажатий клавиш клавиатуры на форме
        /// </summary>
        private void EditServiceTypeForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    btn_Cancel_Click(null, EventArgs.Empty);
                    break;
                case Keys.Enter:
                    btn_Save_Click(null, EventArgs.Empty);
                    break;
            }
        }
    }
}
