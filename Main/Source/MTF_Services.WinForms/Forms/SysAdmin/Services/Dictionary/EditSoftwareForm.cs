using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Services.Dictionary
{
    /// <summary>
    /// Класс формы редактирования программного обеспечения
    /// </summary>
    public partial class EditSoftwareForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private Context _ctx;

        /// <summary>
        /// Наименование программного обеспечения перед изменением
        /// </summary>
        private string _softwareNameBeforeEdit;

        /// <summary>
        /// Наименование типа программного обеспечения перед изменением
        /// </summary>
        private string _softawreTypeBeforeEdit;

        /// <summary>
        /// Список программного обеспечения
        /// </summary>
        public BindingList<SoftwareInfo> Softwares { get; set; }

        /// <summary>
        /// Текущее программное обеспечение
        /// </summary>
        public Software CurrentSoftware { get; set; }

        /// <summary>
        /// Режим работы формы.
        /// </summary>
        private FormMode _formMode;

        /// <summary>
        /// Флаг редактирования программного обеспечения
        /// </summary>
        public bool Edited { get; set; }

        /// <summary>
        /// Конструктор формы редактирования программного обеспечения
        /// </summary>
        public EditSoftwareForm()
        {
            InitializeComponent();

            _ctx = new Context();
            EnDisFields(false);
            BindCollection();
        }

        /// <summary>
        /// Конструктор формы редактирования выбранного программного обеспечения
        /// </summary>
        public EditSoftwareForm(Software selectedSoftware) : this()
        {
            _formMode = FormMode.Edit;
            Text = "Редактирование оперативной памяти";

            InitEditSoftware(selectedSoftware);
            EnDisFields(true);
        }

        /// <summary>
        /// Обновление привязки для редактирования выбранного программного обеспечения
        /// </summary>
        private void InitEditSoftware(Software selectedSoftware)
        {
            CurrentSoftware = selectedSoftware;
            softwareBindingSource.DataSource = CurrentSoftware;

            _softwareNameBeforeEdit = CurrentSoftware.Name;
            _softawreTypeBeforeEdit = CurrentSoftware.SoftType.Name;

            softwareBindingSource.ResumeBinding();
        }

        /// <summary>
        /// Привязка коллекции
        /// </summary>
        private void BindCollection()
        {
            softTypeBindingSource.DataSource = _ctx.GetSoftwareTypes();
            comboBox2.DataSource = softTypeBindingSource;

            Softwares = _ctx.GetSoftwaresInfo();
            softwareInfoBindingSource.DataSource = Softwares;
            dataGridView1.DataSource = softwareInfoBindingSource;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных программного обеспечения
        /// </summary>
        private void softwareInfoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as SoftwareInfo;
            if (selectedItem != null)
            {
                CurrentSoftware = _ctx.GetSoftwareByInfo(selectedItem);
                softwareBindingSource.DataSource = CurrentSoftware;
            }
        }

        /// <summary>
        /// Активация/деактивация полей для редактирования
        /// </summary>
        /// <param name="enabled">Флаг активации полей редактирования</param>
        private void EnDisFields(bool enabled)
        {
            textBox2.Enabled = enabled;
            textBox3.Enabled = enabled;
            comboBox2.Enabled = enabled;
            btn_Cancel.Enabled = enabled;
            btn_Save.Enabled = enabled;

            textBox1.Enabled = !enabled;
            comboBox1.Enabled = !enabled;
            dataGridView1.Enabled = !enabled;
            pictureBox1.Enabled = !enabled;
            pictureBox2.Enabled = !enabled;
            button2.Enabled = !enabled;
            button3.Enabled = !enabled;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который совершает добавление нового программного обеспечения
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            CurrentSoftware = new Software();
            softwareBindingSource.DataSource = CurrentSoftware;
            EnDisFields(true);
            _formMode = FormMode.Add;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который совершает редактирование выбранного программного обеспечения
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (CurrentSoftware != null)
            {
                EnDisFields(true);
                _softawreTypeBeforeEdit = CurrentSoftware.SoftType.Name;
                _softwareNameBeforeEdit = CurrentSoftware.Name;
                _formMode = FormMode.Edit;
            }
            else
                MessageBox.Show("Выберите программное обеспечение из списка или добавьте новое!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену последнего добавления/редактирования
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
                    var selectedSoftType = softTypeBindingSource.Current as SoftType;

                    if (selectedSoftType == null || string.IsNullOrWhiteSpace(CurrentSoftware.Name))
                    {
                        MessageBox.Show("Заполните все поля перед сохранением!", "Предупреждение", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }

                    bool existed = false;

                    switch (_formMode)
                    {
                        case FormMode.Add:
                            existed = await _ctx.CheckSoftwareForDublicate(CurrentSoftware.Name, selectedSoftType.Name);
                            break;
                        case FormMode.Edit:
                            existed = await _ctx.CheckSoftwareForDublicate(CurrentSoftware.Name, selectedSoftType.Name)
                                      && (!CurrentSoftware.Name.Equals(_softwareNameBeforeEdit) || !selectedSoftType.Name.Equals(_softawreTypeBeforeEdit));
                            break;
                    }

                    if (existed)
                    {
                        MessageBox.Show("Программное обеспечение с таким типом и наименованием уже существует!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    switch (_formMode)
                    {
                        case FormMode.Add:
                            await _ctx.AddNewSoftware(CurrentSoftware);
                            break;
                        case FormMode.Edit:
                            await _ctx.EditSoftware(CurrentSoftware);
                            break;
                    }
                    EnDisFields(false);
                    if (_formMode == FormMode.Add)
                        Softwares.Add(new SoftwareInfo
                        {
                            Software = CurrentSoftware.Name,
                            SoftType = selectedSoftType.Name,
                            Cost = CurrentSoftware.Cost
                        });
                    else
                        BindCollection();
                    _formMode = FormMode.None;
                    Edited = true;
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
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditSoftwareForm_FormClosing(object sender, FormClosingEventArgs e)
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
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который совершает подтверждение выбранного программного обеспечения
        /// </summary>
        private void but_Select_Click(object sender, EventArgs e)
        {
            if (CurrentSoftware != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите программное обеспечение из списка или добавьте новое!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит отмену выбранного типа программного обеспечения для поиска
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных типов программного обеспечения
        /// </summary>
        private void softTypeBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == -1)
                ((BindingSource)sender).SuspendBinding();
            else
                ((BindingSource)sender).ResumeBinding();
        }

        /// <summary>
        /// Обработчик события нажатий клавиш клавиатуры на форме
        /// </summary>
        private void EditSoftwareForm_KeyDown(object sender, KeyEventArgs e)
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

        //todo: сделать поиск
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
