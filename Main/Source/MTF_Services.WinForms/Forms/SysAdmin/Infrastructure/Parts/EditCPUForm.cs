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
    /// Класс формы редактирования процессора
    /// </summary>
    public partial class EditCPUForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Редактируемый накопитель
        /// </summary>
        public CPU CurrentCPU { get; set; }

        /// <summary>
        /// Наименование модели процессора перед редактированием
        /// </summary>
        private string _cpuModelBeforeEdit;

        /// <summary>
        /// Наименование производителя процессора перед редактированием
        /// </summary>
        private string _cpuManufacturerBeforeEdit;

        /// <summary>
        /// Режим работы формы.
        /// </summary>
        private FormMode _formMode;

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновке в источнике данных производителей
        /// </summary>
        private bool _afterManufacturerSuspend;

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновке в источнике данных разъемов процессоров
        /// </summary>
        private bool _afterCpuSocketSuspend;

        /// <summary>
        /// Конструктор формы создания нового процессора
        /// </summary>
        public EditCPUForm()
        {
            InitializeComponent();
            btn_Cancel.Image = new Bitmap(Resources.no, 20, 20);
            btn_Save.Image = new Bitmap(Resources.camera_test, 20, 20);

            _ctx = new Context();

            BindAll();

            CurrentCPU = new CPU();
            cPUBindingSource.DataSource = CurrentCPU;

            _formMode = FormMode.Add;
            Text = "Создание процессора";
        }

        /// <summary>
        /// Конструктор формы редактирования выбранного процессора
        /// </summary>
        /// <param name="selectedCPU">Выбранный процессор</param>
        public EditCPUForm(CPU selectedCPU) : this()
        {
            _formMode = FormMode.Edit;
            Text = "Редактирование процессора";

            InitEditCPU(selectedCPU);
        }

        #region Bindings

        /// <summary>
        /// Инициализация привязок всех коллекций
        /// </summary>
        private void BindAll()
        {
            BindManufacturer();
            BindCPUSockets();

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
        /// Инициализация привязки коллекции с разъемами процессоров
        /// </summary>
        private void BindCPUSockets()
        {
            cpuSocketBindingSource.DataSource = _ctx.GetCPUSockets();
            comboBox2.DataSource = cpuSocketBindingSource;
        }

        /// <summary>
        /// Инициализация привязок для редактирования выбраного процессора
        /// </summary>
        private void InitEditCPU(CPU selectedCPU)
        {
            CurrentCPU = selectedCPU;
            cPUBindingSource.DataSource = CurrentCPU;

            _cpuManufacturerBeforeEdit = CurrentCPU.Manufacturer.Name;
            _cpuModelBeforeEdit = CurrentCPU.Model;

            cPUBindingSource.ResumeBinding();
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
        /// Обработчик события изменения текущего элемента в источнике данных разъемов процессоров
        /// </summary>
        private void cpuSocketBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (cpuSocketBindingSource.IsBindingSuspended)
            {
                _afterCpuSocketSuspend = true;
                cpuSocketBindingSource.ResumeBinding();
            }
            else if (_afterCpuSocketSuspend)
            {
                _afterCpuSocketSuspend = false;
                cpuSocketBindingSource.Position = comboBox2.SelectedIndex;
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
            if (CurrentCPU != null)
                CurrentCPU.ManufacturerId = 0;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление выбранного разъема процессора
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            cpuSocketBindingSource.SuspendBinding();
            if (CurrentCPU != null)
                CurrentCPU.CpuSocketId = 0;
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditCPUForm_FormClosing(object sender, FormClosingEventArgs e)
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
        /// который производит сохранение добавляемой / редактируемой записи об процессоре
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит добавление выбранного интерфейса к доступным
        /// </summary>
        private async void btn_Save_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(CurrentCPU.Model) ||
                CurrentCPU.Price <= 0 || CurrentCPU.Frequency <= 0 || CurrentCPU.Frequency <= 0)
            {
                MessageBox.Show("Заполните все поля для сохранения изменений!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                var selectedManufacturer = manufacturerBindingSource.Current as Manufacturer;
                var selectedCPUSocket = cpuSocketBindingSource.Current as CpuSocket;
                bool existed = false;

                if (!selectedManufacturer.Name.Equals(selectedCPUSocket.Manufacturer.Name))
                {
                    MessageBox.Show("Производитель процессора не совпадает с производителем разъема!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                switch (_formMode)
                {
                    case FormMode.Add:
                        existed = await _ctx.CheckCPUForDublicate(CurrentCPU.Model, selectedManufacturer.Name);
                        break;
                    case FormMode.Edit:
                        existed = await _ctx.CheckCPUForDublicate(CurrentCPU.Model, selectedManufacturer.Name)
                                  && (!CurrentCPU.Model.Equals(_cpuModelBeforeEdit) ||
                                      !selectedManufacturer.Name.Equals(_cpuManufacturerBeforeEdit));
                        break;
                }

                if (existed)
                {
                    MessageBox.Show("Процессор с таким производителем и моделью уже существует!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                switch (_formMode)
                {
                    case FormMode.Add:
                        await _ctx.AddNewCPU(CurrentCPU);
                        MessageBox.Show("Новый процессор успешно сохранен!", "Информация", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        break;
                    case FormMode.Edit:
                        await _ctx.EditCPU(CurrentCPU);
                        MessageBox.Show("Изменения в процессоре успешно сохранены!", "Информация", MessageBoxButtons.OK,
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
        /// который открывает диалоговое окно редактирования производителей
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
        /// который открывает диалоговое окно редактирования разъемов процессора
        /// </summary>
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            var editCpuSocket = new EditCPUSocketForm();
            if (editCpuSocket.ShowDialog() == DialogResult.OK)
            {
                var cpuSocket = cpuSocketBindingSource.Current as CpuSocket;
                BindCPUSockets();
                if (cpuSocket != null)
                {
                    int pos = cpuSocketBindingSource.IndexOf(cpuSocket);
                    if (pos > -1)
                        cpuSocketBindingSource.Position = pos;
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно поиска разъема процессора
        /// </summary>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var editCpuSocket = new EditCPUSocketForm(true);
            if (editCpuSocket.ShowDialog() == DialogResult.OK)
            {
                if (editCpuSocket.Edited)
                    BindCPUSockets();

                cpuSocketBindingSource_CurrentChanged(cpuSocketBindingSource, EventArgs.Empty);
                cpuSocketBindingSource.Position =
                    cpuSocketBindingSource.IndexOf(editCpuSocket.CurrentSocket);
            }
        }

        /// <summary>
        /// Обработчик события форматирования отображаемого значения в списке разъемов процессоров
        /// </summary>
        private void comboBox2_Format(object sender, ListControlConvertEventArgs e)
        {
            var socket = e.ListItem as CpuSocket;
            if (socket != null)
                e.Value = $"{socket.Manufacturer.Name} {socket.Name}";
        }

        /// <summary>
        /// Обработчик нажатий клавиш на форме
        /// </summary>
        private void EditCPUForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Enter:
                    btn_Save_Click(null, EventArgs.Empty);
                    break;
            }
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
        /// Обработчик события нажатия клавиши в numericupdown с количеством ядер,
        /// который ограничивает ввод только цифр и управляющих символов
        /// </summary>
        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши в текстовом поле с тактовой частотой и ценой,
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
                if (e.KeyChar == (char)Keys.Back)
                    return;
            e.Handled = true;
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
        /// Обработчик события изменения текущего индекса в списке разъемов процессора
        /// </summary>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > -1)
                cpuSocketBindingSource_CurrentChanged(cpuSocketBindingSource, EventArgs.Empty);
        }
    }
}