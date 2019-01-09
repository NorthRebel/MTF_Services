using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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
    /// Класс формы редактирования платформы
    /// </summary>
    public partial class EditPlatformForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Наименование модели платформы перед редактированием
        /// </summary>
        private string _modelNameBeforeEdit;

        /// <summary>
        /// Наименование производителя платформы перед редактированием
        /// </summary>
        private string _manufacturerBeforeEdit;

        /// <summary>
        /// Редактируемуя платформа
        /// </summary>
        public Platform CurrentPlatform { get; set; }

        /// <summary>
        /// Список поддерживаемых интерфейсов
        /// </summary>
        public BindingList<AvalibleInterface> AvalibleInterfaces { get; set; }

        /// <summary>
        /// Режим работы формы.
        /// </summary>
        private FormMode _formMode;

        /// <summary>
        /// Флаг сохранения изменений
        /// </summary>
        private bool _saved;

        /// <summary>
        /// Флаг редактирования справочников или добавления/изменения платформ
        /// </summary>
        public bool Edited { get; set; }

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновке в источнике данных производителей
        /// </summary>
        private bool _afterManufacturerSuspend;

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновке в источнике данных разъемов процессоров
        /// </summary>
        private bool _afterCpuSocketSuspend;

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновке в источнике данных типов оперативной памяти
        /// </summary>
        private bool _afterRamTypeSuspend;

        /// <summary>
        /// Конструктор формы создания новой платформы
        /// </summary>
        public EditPlatformForm()
        {
            InitializeComponent();
            btn_Cancel.Image = new Bitmap(Resources.no, 20, 20);
            btn_Save.Image = new Bitmap(Resources.camera_test, 20, 20);

            _ctx = new Context();

            BindAll();

            CurrentPlatform = new Platform();
            platformBindingSource.DataSource = CurrentPlatform;

            _formMode = FormMode.Add;
            Text = "Создание новой платформы";
        }

        /// <summary>
        /// Конструктор формы редактирования выбранной  платформы
        /// </summary>
        /// <param name="selectedPlatform">Выбранная платформа</param>
        public EditPlatformForm(PlatformInfo selectedPlatform) : this()
        {
            _formMode = FormMode.Edit;
            Text = "Редактирование платформы";

            InitEditPlatform(selectedPlatform);
        }

        #region Bindings

        /// <summary>
        /// Инициализация привязок для редактирования выбранной платформы
        /// </summary>
        private void InitEditPlatform(PlatformInfo selectedPlatform)
        {
            CurrentPlatform = _ctx.GetPlatformByPlatformInfo(selectedPlatform);
            platformBindingSource.DataSource = CurrentPlatform;

            _manufacturerBeforeEdit = CurrentPlatform.Manufacturer.Name;
            _modelNameBeforeEdit = selectedPlatform.Model;

            BindAvalibleInterfacesForEdit();

            platformBindingSource.ResumeBinding();
        }

        /// <summary>
        /// Инициализация привязок всех коллекций
        /// </summary>
        private void BindAll()
        {
            BindCpuSockets();
            BindManufacturer();
            BindRamTypes();
            BindStorageInterfaces();
            BindAvalibleInterfacesNew();

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
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
        private void BindCpuSockets()
        {
            cpuSocketBindingSource.DataSource = _ctx.GetCPUSockets();
            comboBox2.DataSource = cpuSocketBindingSource;
        }

        /// <summary>
        /// Инициализация привязки коллекции с типами оперативной памяти
        /// </summary>
        private void BindRamTypes()
        {
            ramTypeBindingSource.DataSource = _ctx.GetRamsTypes();
            comboBox3.DataSource = ramTypeBindingSource;
        }

        /// <summary>
        /// Инициализация привязки коллекции с интерфейсами накопителей
        /// </summary>
        private void BindStorageInterfaces()
        {
            strorageInterfaceBindingSource.DataSource = _ctx.GetStorageInterfaces();
            listBox1.DataSource = strorageInterfaceBindingSource;
        }

        /// <summary>
        /// Привязка коллекции доступных интерфейсов накопителей для новой платформы
        /// </summary>
        private void BindAvalibleInterfacesNew()
        {
            AvalibleInterfaces = new BindingList<AvalibleInterface>();
            avalibleInterfaceBindingSource.DataSource = AvalibleInterfaces;
            dataGridView1.DataSource = avalibleInterfaceBindingSource;
        }


        /// <summary>
        /// Привязка коллекции доступных интерфейсов накопителей для редактируемой платформы
        /// </summary>
        private void BindAvalibleInterfacesForEdit()
        {
            AvalibleInterfaces = _ctx.GetAvalibleInterfacesOfPlarformBS(CurrentPlatform.Platform_StorageInt);
            avalibleInterfaceBindingSource.DataSource = AvalibleInterfaces;
            dataGridView1.DataSource = avalibleInterfaceBindingSource;
        }

        #endregion

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно поиска производителя
        /// </summary>
        private void pictureBox17_Click(object sender, EventArgs e)
        {
            var findManufacturer = new EditManufacturerForm(true);
            if (findManufacturer.ShowDialog() == DialogResult.OK)
            {
                if (findManufacturer.Edited)
                {
                    BindManufacturer();
                    Edited = true;
                }

                manufacturerBindingSource_CurrentChanged(manufacturerBindingSource, EventArgs.Empty);
                manufacturerBindingSource.Position =
                    ((BindingList<Manufacturer>)manufacturerBindingSource.DataSource).IndexOf(findManufacturer.CurrentManufacturer);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно поиска разъема процессора
        /// </summary>
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            var cpuSocket = new EditCPUSocketForm(true);
            if (cpuSocket.ShowDialog() == DialogResult.OK)
            {
                if (cpuSocket.Edited)
                {
                    BindCpuSockets();
                    Edited = true;
                }

                cpuSocketBindingSource_CurrentChanged(cpuSocketBindingSource, EventArgs.Empty);
                cpuSocketBindingSource.Position =
                    ((BindingList<CpuSocket>)cpuSocketBindingSource.DataSource).IndexOf(cpuSocket.CurrentSocket);
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который открывает диалоговое окно поиска типа оперативной памяти
        /// </summary>
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            var ramType = new EditRAMTypeForm(true);
            if (ramType.ShowDialog() == DialogResult.OK)
            {
                if (ramType.Edited)
                {
                    BindRamTypes();
                    Edited = true;
                }

                ramTypeBindingSource_CurrentChanged(ramTypeBindingSource, EventArgs.Empty);
                ramTypeBindingSource.Position =
                    ((BindingList<RamType>)ramTypeBindingSource.DataSource).IndexOf(ramType.CurrentRamType);
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
            if (CurrentPlatform != null)
                CurrentPlatform.ManufacturerId = 0;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление выбранного разъема процессора
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            cpuSocketBindingSource.SuspendBinding();
            if (CurrentPlatform != null)
                CurrentPlatform.CpuSocketId = 0;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление выбранного типа оперативной памяти
        /// </summary>
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            comboBox3.SelectedIndex = -1;
            ramTypeBindingSource.SuspendBinding();
            if (CurrentPlatform != null)
                CurrentPlatform.RamTypeId = 0;
        }

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
                ramTypeBindingSource.Position = comboBox3.SelectedIndex;
            }
        }

        /// <summary>
        /// Обработчик события форматирования отображаемого значения в списке разъемов процессоров
        /// </summary>
        private void comboBox2_Format(object sender, ListControlConvertEventArgs e)
        {
            var cpuToFormat = e.ListItem as CpuSocket;
            if (cpuToFormat != null)
                e.Value = $"{cpuToFormat.Manufacturer.Name} {cpuToFormat.Name}";
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит добавление выбранного интерфейса к доступным
        /// </summary>
        private void pictureBox7_Click(object sender, EventArgs e)
        {
            var selectedInterface = strorageInterfaceBindingSource.Current as StrorageInterface;
            if (selectedInterface != null)
            {
                var existedInt = AvalibleInterfaces.SingleOrDefault(ai => ai.Name.Equals(selectedInterface.Name));
                if (existedInt != null)
                {
                    existedInt.Slot_Count++;
                    AvalibleInterfaces.Remove(existedInt);
                    AvalibleInterfaces.Add(existedInt);
                }
                else
                    AvalibleInterfaces.Add(new AvalibleInterface
                    {
                        Name = selectedInterface.Name,
                        Slot_Count = 1
                    });
            }
            else
                MessageBox.Show("Выберите интерфейс из списка!", "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит увеличение количества выбранного интерфейса
        /// </summary>
        private void pictureBox21_Click(object sender, EventArgs e)
        {
            var selectedInterface = avalibleInterfaceBindingSource.Current as AvalibleInterface;
            if (selectedInterface != null)
            {
                selectedInterface.Slot_Count++;
                AvalibleInterfaces.Remove(selectedInterface);
                AvalibleInterfaces.Add(selectedInterface);
            }
            else
                MessageBox.Show("Выберите интерфейс из списка или добавьте новый!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит уменьшение количества выбранного интерфейса
        /// </summary>
        private void pictureBox20_Click(object sender, EventArgs e)
        {
            var selectedInterface = avalibleInterfaceBindingSource.Current as AvalibleInterface;
            if (selectedInterface != null)
            {
                selectedInterface.Slot_Count--;
                AvalibleInterfaces.Remove(selectedInterface);
                if (selectedInterface.Slot_Count > 0)
                    AvalibleInterfaces.Add(selectedInterface);
            }
            else
                MessageBox.Show("Выберите интерфейс из списка или добавьте новый!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который производит удаление выбранного интерфейса
        /// </summary>
        private void pictureBox19_Click(object sender, EventArgs e)
        {
            var selectedInterface = avalibleInterfaceBindingSource.Current as AvalibleInterface;
            if (selectedInterface != null)
            {
                var result = MessageBox.Show("Выбранный интерфейс будет удален! Продолжить?",
                    "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                    AvalibleInterfaces.Remove(selectedInterface);
            }
            else
                MessageBox.Show("Выберите интерфейс из списка или добавьте новый!",
                    "Предупреждение", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит сохранение добавляемой / редактируемой записи об платформе
        /// </summary>
        private async void btn_Save_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(CurrentPlatform.Model) || CurrentPlatform.RamVolumeMax <= 0 ||
                CurrentPlatform.RamSocketCount <= 0 ||
                CurrentPlatform.Price <= 0 || CurrentPlatform.CPUCount <= 0)
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
                        existed = await _ctx.CheckPlatformForDublicate(CurrentPlatform.Model, selectedManufacturer.Name);
                        break;
                    case FormMode.Edit:
                        existed = await _ctx.CheckPlatformForDublicate(CurrentPlatform.Model, selectedManufacturer.Name)
                                  && (!CurrentPlatform.Model.Equals(_modelNameBeforeEdit) || !selectedManufacturer.Name.Equals(_manufacturerBeforeEdit));
                        break;
                }

                if (existed)
                {
                    MessageBox.Show("Платформа с таким производителем и моделью уже существует!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                CurrentPlatform.Platform_StorageInt = _ctx.GetPlatformStorageIntFromAvalible(CurrentPlatform, AvalibleInterfaces.ToList());

                switch (_formMode)
                {
                    case FormMode.Add:
                        await _ctx.AddNewPlatform(CurrentPlatform);
                        MessageBox.Show("Новая платформа успешно сохранена!", "Информация", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        break;
                    case FormMode.Edit:
                        await _ctx.EditPlatform(CurrentPlatform);
                        MessageBox.Show("Изменения в платформе успешно сохранены!", "Информация", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                        break;
                }

                _saved = true;
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
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditPlatformForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_saved)
            {
                var result = MessageBox.Show("Изменения не будут сохранены! Продолжить?", "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену создания / редактирования платформы, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
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

        /// <summary>
        /// Обработчик события изменения текущего индекса в списке типов оперативной памяти
        /// </summary>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex > -1)
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
        /// Обработчик события нажатия клавиши в текстовом поле с макс. количеством ОЗУ,
        /// который ограничивает ввод только цифр и управляющих символов
        /// </summary>
        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || char.IsControl(e.KeyChar)))
                e.Handled = true;
        }

        /// <summary>
        /// Обработчик события нажатия клавиши в numericupdown с количеством разъемов,
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
