using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Dictionary
{
    /// <summary>
    /// Класс формы редактирования разъемов процессоров
    /// </summary>
    public partial class EditCPUSocketForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Наименование разъема перед изменением
        /// </summary>
        private string _socketNameBeforeEdit;

        /// <summary>
        /// Наименование производителя разъема перед изменением
        /// </summary>
        private string _socketManufacturerBeforeEdit;

        /// <summary>
        /// Список раъемов
        /// </summary>
        public BindingList<CPUSocketInfo> CpuSockets { get; set; }

        /// <summary>
        /// Текущий разъем
        /// </summary>
        public CpuSocket CurrentSocket { get; set; }

        /// <summary>
        /// Режим работы формы.
        /// </summary>
        private FormMode _formMode;

        /// <summary>
        /// Индекс последнего выбранного элемента перед добавлением новой записи
        /// </summary>
        private int _lastSelectedIndex;

        /// <summary>
        /// Флаг редактирования разъемов
        /// </summary>
        public bool Edited { get; set; }

        /// <summary>
        /// Конструктор формы редактирования разъемов процессоров
        /// </summary>
        public EditCPUSocketForm()
        {
            InitializeComponent();
            _formMode = FormMode.None;
            _ctx = new Context();
            EnDisFields(false);
            BindManufacturersToFind();
            BindCollection();
        }

        /// <summary>
        /// Конструктор для выбора разъема процессора из списка
        /// </summary>
        /// <param name="findMode">Флаг поиска разъема</param>
        public EditCPUSocketForm(bool findMode) : this()
        {
            if (!findMode)
                throw new ArgumentException("Неверное значение флага!");
            _formMode = FormMode.None;
            but_Select.Visible = findMode;
            but_Select.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
        }

        /// <summary>
        /// Привязка коллекции разъемов процессора
        /// </summary>
        private void BindCollection()
        {
            manufacturerBindingSource.DataSource = _ctx.GetManufacturers();
            comboBox2.DataSource = manufacturerBindingSource;

            CpuSockets = _ctx.GetCPUSocketsInfo();
            cPUSocketInfoBindingSource.DataSource = CpuSockets;
            dataGridView1.DataSource = cPUSocketInfoBindingSource;
        }

        /// <summary>
        /// Привязка коллекции производителей для поиска записей
        /// </summary>
        private void BindManufacturersToFind()
        {
            manufaturerToFindBS.DataSource = _ctx.GetManufacturers();
            comboBox_ManufacturersToFind.DataSource = manufaturerToFindBS;
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных разъемов процессоров
        /// </summary>
        private async void cPUSocketInfoBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            var selectedItem = ((BindingSource)sender).Current as CPUSocketInfo;
            if (selectedItem != null)
            {
                try
                {
                    _lastSelectedIndex = CpuSockets.IndexOf(selectedItem);
                    CurrentSocket = await _ctx.GetCPUSocketByInfo(selectedItem);
                    cpuSocketBindingSource.DataSource = CurrentSocket;
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Активация/деактивация полей для редактирования
        /// </summary>
        /// <param name="enabled">Флаг активации полей редактирования</param>
        private void EnDisFields(bool enabled)
        {
            textBox2.Enabled = enabled;
            comboBox2.Enabled = enabled;
            btn_Cancel.Enabled = enabled;
            btn_Save.Enabled = enabled;

            textBox_ConditionToFind.Enabled = !enabled;
            comboBox_ManufacturersToFind.Enabled = !enabled;
            dataGridView1.Enabled = !enabled;
            picBtn_FindByCondition.Enabled = !enabled;
            picBtn_ClearCondition.Enabled = !enabled;
            button2.Enabled = !enabled;
            button3.Enabled = !enabled;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который осуществляет добавление нового разъема
        /// </summary>
        private void button2_Click(object sender, EventArgs e)
        {
            CurrentSocket = new CpuSocket();
            cpuSocketBindingSource.DataSource = CurrentSocket;
            EnDisFields(true);
            _formMode = FormMode.Add;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который осуществляет редактирование выбранного разъема процессора
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            if (CurrentSocket != null)
            {
                EnDisFields(true);
                _socketManufacturerBeforeEdit = CurrentSocket.Manufacturer.Name;
                _socketNameBeforeEdit = CurrentSocket.Name;
                _formMode = FormMode.Edit;
            }
            else
                MessageBox.Show("Выберите элемент из списка или добавьте новый!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            CurrentSocket = _ctx.CancelChanges(CurrentSocket);
                            cpuSocketBindingSource.DataSource = CurrentSocket;
                            cpuSocketBindingSource.ResetBindings(true);
                        }
                        else
                        {
                            if (_lastSelectedIndex > -1)
                                cPUSocketInfoBindingSource.Position = _lastSelectedIndex;
                        }
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
        /// который выполняет сохранение изменений
        /// </summary>
        private async void btn_Save_Click(object sender, EventArgs e)
        {
            if (_formMode != FormMode.None)
            {
                try
                {
                    var selectedManufacturer = manufacturerBindingSource.Current as Manufacturer;

                    if (selectedManufacturer == null || string.IsNullOrWhiteSpace(CurrentSocket.Name))
                    {
                        MessageBox.Show("Заполните все поля перед сохранением!", "Предупреждение", MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        return;
                    }

                    bool existed = false;

                    switch (_formMode)
                    {
                        case FormMode.Add:
                            existed = await _ctx.CheckCpuSocketForDublicate(CurrentSocket.Name, selectedManufacturer.Name);
                            break;
                        case FormMode.Edit:
                            existed = await _ctx.CheckCpuSocketForDublicate(CurrentSocket.Name, selectedManufacturer.Name)
                                      && (!CurrentSocket.Name.Equals(_socketNameBeforeEdit) || !selectedManufacturer.Name.Equals(_socketManufacturerBeforeEdit));
                            break;
                    }

                    if (existed)
                    {
                        MessageBox.Show("Разъем с таким производителем и наименованием уже существует!", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    switch (_formMode)
                    {
                        case FormMode.Add:
                            await _ctx.AddNewCPUSocket(CurrentSocket);
                            break;
                        case FormMode.Edit:
                            await _ctx.EditCPUSocket(CurrentSocket);
                            break;
                    }
                    EnDisFields(false);
                    if (_formMode == FormMode.Add)
                        CpuSockets.Add(new CPUSocketInfo
                        {
                            Socket = CurrentSocket.Name,
                            Manufacturer = selectedManufacturer.Name
                        });
                    else
                        BindCollection();
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
        private void EditCPUSocketForm_FormClosing(object sender, FormClosingEventArgs e)
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
        /// который совершает подтверждение выбранного разъема
        /// </summary>
        private void but_Select_Click(object sender, EventArgs e)
        {
            if (CurrentSocket != null)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Выберите элемент из списка или добавьте новый!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит очистку строки для поиска
        /// </summary>
        private void picBtn_ClearCondition_Click(object sender, EventArgs e)
        {
            textBox_ConditionToFind.Text = string.Empty;
            comboBox_ManufacturersToFind.SelectedIndex = -1;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит поиск разъема по наименованию и/или производителю
        /// </summary>
        private void picBtn_FindByCondition_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_ConditionToFind.Text) && comboBox_ManufacturersToFind.SelectedIndex < 0)
            {
                MessageBox.Show("Введите условие для поиска разъема процессора!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            CPUSocketInfo firstFoundedElement = null;

            if (!string.IsNullOrWhiteSpace(textBox_ConditionToFind.Text) && comboBox_ManufacturersToFind.SelectedIndex >= 0)
                firstFoundedElement = CpuSockets.
                    FirstOrDefault(x => x.Socket.ToUpper().Contains(textBox_ConditionToFind.Text.ToUpper())
                                        && x.Manufacturer.Equals((manufaturerToFindBS.Current as Manufacturer).Name));
            else if (!string.IsNullOrWhiteSpace(textBox_ConditionToFind.Text))
                firstFoundedElement = CpuSockets.FirstOrDefault(x => x.Socket.ToUpper().Contains(textBox_ConditionToFind.Text.ToUpper()));
            else if (comboBox_ManufacturersToFind.SelectedIndex >= 0)
                firstFoundedElement = CpuSockets.
                    FirstOrDefault(x => x.Manufacturer.Equals((manufaturerToFindBS.Current as Manufacturer).Name));

            if (firstFoundedElement != null)
                cPUSocketInfoBindingSource.Position = CpuSockets.IndexOf(firstFoundedElement);
            else
                MessageBox.Show("Не удалось найти разъем процессора по указанному условию!", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных разъемов для поиска
        /// </summary>
        private void manufaturerToFindBS_CurrentChanged(object sender, EventArgs e)
        {
            if (comboBox_ManufacturersToFind.SelectedIndex == -1)
                ((BindingSource)sender).SuspendBinding();
            else
                ((BindingSource)sender).ResumeBinding();
        }

        /// <summary>
        /// Ограничение ввода в наименовании добавляемого/редактируемого разъема процессора 
        /// </summary>
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsLetter(e.KeyChar) && e.KeyChar != (int)Keys.Space && !Char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
