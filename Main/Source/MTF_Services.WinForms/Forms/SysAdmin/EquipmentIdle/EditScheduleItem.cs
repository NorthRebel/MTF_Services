using System;
using System.Drawing;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.EquipmentIdle
{
    /// <summary>
    /// Класс формы редактирования позиции расписания обслуживания
    /// </summary>
    public partial class EditScheduleItem : Form
    {
        private readonly SAN _selectedSan;
        private readonly Server _selectedServer;
        private readonly SAN_MaintenanceShedule _sanMaintenanceShedule;
        private readonly Server_MaintenanceShedule _serverMaintenanceShedule;

        /// <summary>
        /// Тип редактируемого расписания
        /// </summary>
        private readonly ScheduleEditType _scheduleEditType;

        /// <summary>
        /// Режим использования формы
        /// </summary>
        private readonly FormMode _formMode;

        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Дата начала обслуживания
        /// </summary>
        private DateTime _beginDateTime;

        /// <summary>
        /// Дата окончания обслуживания
        /// </summary>
        private DateTime _endDateTime;

        /// <summary>
        /// Конструктор формы редактирования позиции расписания обслуживания
        /// </summary>        
        private EditScheduleItem(ScheduleEditType scheduleEditType)
        {
            _scheduleEditType = scheduleEditType;
            InitializeComponent();

            _ctx = new Context();

            btn_OK.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));

            comboBox_DurationType.SelectedIndex = 0;
            dateTimePicker_Begin.MinDate = DateTime.Now;
            dateTimePicker_End.MinDate = DateTime.Now;
            numericUpDown_BeginHours.Minimum = DateTime.Now.Hour;
            numericUpDown_BeginMinutes.Minimum = DateTime.Now.Minute;
            numericUpDown_EndHours.Minimum = DateTime.Now.Hour;
            numericUpDown_EndMinutes.Minimum = DateTime.Now.Minute;

            SetEndDate();

            _formMode = FormMode.Add;
            switch (scheduleEditType)
            {
                case ScheduleEditType.Server:
                    Text = "Планирование обслуживания сервера";
                    break;
                case ScheduleEditType.SAN:
                    Text = "Планирование обслуживания хранилища данных";
                    break;
            }
        }

        /// <summary>
        /// Конструктор добавления новой позиции расписания обслуживания севера
        /// </summary>
        /// <param name="selectedServer">Экземпляр выбранной конфигурации сервера</param>
        public EditScheduleItem(Server selectedServer) : this(ScheduleEditType.Server)
        {
            _selectedServer = selectedServer;
        }

        /// <summary>
        /// Конструктор добавления новой позиции расписания обслуживания хранилища данных
        /// </summary>
        /// <param name="selectedSan">Экземпляр выбранного хранилища данных</param>
        public EditScheduleItem(SAN selectedSan) : this(ScheduleEditType.SAN)
        {
            _selectedSan = selectedSan;
        }

        /// <summary>
        /// Конструктор редактирования позиции расписания обслуживания севера
        /// </summary>
        /// <param name="serverMaintenanceShedule">Выбранная позиция расписания обслуживания севера</param>
        public EditScheduleItem(Server_MaintenanceShedule serverMaintenanceShedule) : this(ScheduleEditType.Server)
        {
            _serverMaintenanceShedule = serverMaintenanceShedule;

            UnSubscribeEvents();
            SetMinimumValuesToDefault();

            dateTimePicker_Begin.Value = _serverMaintenanceShedule.BeginPeriod;
            numericUpDown_BeginHours.Value = _serverMaintenanceShedule.BeginPeriod.Hour;
            numericUpDown_BeginMinutes.Value = _serverMaintenanceShedule.BeginPeriod.Minute;

            dateTimePicker_End.Value = _serverMaintenanceShedule.EndPeriod;
            numericUpDown_EndHours.Value = _serverMaintenanceShedule.EndPeriod.Hour;
            numericUpDown_EndMinutes.Value = _serverMaintenanceShedule.EndPeriod.Minute;

            CalculateDuration();
            SubscribeEvents();
            _formMode = FormMode.Edit;
        }

        /// <summary>
        /// Конструктор редактирования позиции расписания обслуживания хранилища данных
        /// </summary>
        /// <param name="sanMaintenanceShedule">Выбранная позиция расписания обслуживания хранилища данных</param>
        public EditScheduleItem(SAN_MaintenanceShedule sanMaintenanceShedule) : this(ScheduleEditType.Server)
        {
            _sanMaintenanceShedule = sanMaintenanceShedule;

            UnSubscribeEvents();
            SetMinimumValuesToDefault();

            dateTimePicker_Begin.Value = _sanMaintenanceShedule.BeginPeriod;
            numericUpDown_BeginHours.Value = _sanMaintenanceShedule.BeginPeriod.Hour;
            numericUpDown_BeginMinutes.Value = _sanMaintenanceShedule.BeginPeriod.Minute;

            dateTimePicker_End.Value = _sanMaintenanceShedule.EndPeriod;
            numericUpDown_EndHours.Value = _sanMaintenanceShedule.EndPeriod.Hour;
            numericUpDown_EndMinutes.Value = _sanMaintenanceShedule.EndPeriod.Minute;

            CalculateDuration();
            SubscribeEvents();
            _formMode = FormMode.Edit;
        }

        /// <summary>
        /// Установка минимальных значений для компонентов по умолчанию
        /// </summary>
        private void SetMinimumValuesToDefault()
        {
            numericUpDown_BeginHours.Minimum = 0;
            numericUpDown_BeginMinutes.Minimum = 0;
            numericUpDown_EndHours.Minimum = 0;
            numericUpDown_EndMinutes.Minimum = 0;
        }

        /// <summary>
        /// Отписка обработчиков событий изменений значений компонентов
        /// </summary>
        private void UnSubscribeEvents()
        {
            dateTimePicker_Begin.ValueChanged -= dateTimePicker_Begin_ValueChanged;
            dateTimePicker_End.ValueChanged -= dateTimePicker_End_ValueChanged;
            numericUpDown_BeginHours.ValueChanged -= numericUpDown_BeginHours_ValueChanged;
            numericUpDown_BeginMinutes.ValueChanged -= numericUpDown_BeginMinutes_ValueChanged;
            numericUpDown_EndHours.ValueChanged -= numericUpDown_EndHours_ValueChanged;
            numericUpDown_EndMinutes.ValueChanged -= numericUpDown_EndMinutes_ValueChanged;
        }

        /// <summary>
        /// Подписка обработчиков событий изменений значений компонентов
        /// </summary>
        private void SubscribeEvents()
        {
            dateTimePicker_Begin.ValueChanged += dateTimePicker_Begin_ValueChanged;
            dateTimePicker_End.ValueChanged += dateTimePicker_End_ValueChanged;
            numericUpDown_BeginHours.ValueChanged += numericUpDown_BeginHours_ValueChanged;
            numericUpDown_BeginMinutes.ValueChanged += numericUpDown_BeginMinutes_ValueChanged;
            numericUpDown_EndHours.ValueChanged += numericUpDown_EndHours_ValueChanged;
            numericUpDown_EndMinutes.ValueChanged += numericUpDown_EndMinutes_ValueChanged;
        }

        /// <summary>
        /// Обработчик события изменения даты начала обслуживания,
        /// который осуществляет корректировку допустимой даты окончания обслуживания и расчет длительности
        /// </summary>
        private void dateTimePicker_Begin_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker_Begin.Value > DateTime.Now)
            {
                numericUpDown_BeginHours.Minimum = 0;
                numericUpDown_BeginMinutes.Minimum = 0;

                if (numericUpDown_BeginHours.Value == 23 && numericUpDown_BeginMinutes.Value == 59)
                    dateTimePicker_End.MinDate = dateTimePicker_Begin.Value.AddDays(1);
                else
                    SetEndDate();
            }
            else
            {
                dateTimePicker_Begin.MinDate = DateTime.Now;
                dateTimePicker_End.MinDate = DateTime.Now;
                numericUpDown_BeginHours.Minimum = DateTime.Now.Hour;
                numericUpDown_BeginMinutes.Minimum = DateTime.Now.Minute;
                numericUpDown_EndHours.Minimum = DateTime.Now.Hour;
                numericUpDown_EndMinutes.Minimum = DateTime.Now.Minute;
                CalculateDuration();
            }
        }

        /// <summary>
        /// Корректировка допустимой даты
        /// </summary>
        private void SetEndDate()
        {
            if (numericUpDown_BeginHours.Value == 23 && numericUpDown_BeginMinutes.Value == 59)
                dateTimePicker_End.MinDate = dateTimePicker_Begin.Value.AddDays(1);
            else
            {
                dateTimePicker_End.MinDate = dateTimePicker_Begin.Value;

                if (numericUpDown_BeginMinutes.Value == 59)
                {
                    numericUpDown_EndMinutes.Minimum = 0;
                    numericUpDown_EndHours.Minimum = numericUpDown_BeginHours.Value + 1;
                }
                else if (dateTimePicker_End.Value > dateTimePicker_Begin.Value)
                {
                    numericUpDown_EndHours.Minimum = 0;
                    numericUpDown_EndMinutes.Minimum = 0;
                }
                else
                {
                    if (numericUpDown_EndHours.Value == numericUpDown_BeginHours.Value)
                        numericUpDown_EndMinutes.Minimum = numericUpDown_BeginMinutes.Value;
                    numericUpDown_EndHours.Minimum = numericUpDown_BeginHours.Value;
                }
            }

            CalculateDuration();
        }

        /// <summary>
        /// Расчет разницы между окончанием и началом обслуживания
        /// </summary>
        private void CalculateDuration()
        {
            _beginDateTime = new DateTime
            (
                dateTimePicker_Begin.Value.Year,
                dateTimePicker_Begin.Value.Month,
                dateTimePicker_Begin.Value.Day,
                (int)numericUpDown_BeginHours.Value,
                (int)numericUpDown_BeginMinutes.Value,
                0
            );

            _endDateTime = new DateTime
            (
                dateTimePicker_End.Value.Year,
                dateTimePicker_End.Value.Month,
                dateTimePicker_End.Value.Day,
                (int)numericUpDown_EndHours.Value,
                (int)numericUpDown_EndMinutes.Value,
                0
            );

            TimeSpan difference = _endDateTime - _beginDateTime;

            switch (comboBox_DurationType.SelectedIndex)
            {
                case 0:
                    textBox_Duration.Text = difference.TotalMinutes.ToString();
                    break;
                case 1:
                    textBox_Duration.Text = TimeSpan.FromHours(difference.TotalHours).ToString(@"hh\:mm");
                    break;
            }
        }

        /// <summary>
        /// Обработчик события изменения часа начала обслуживания,
        /// который осуществляет корректировку допустимой даты окончания обслуживания и расчет длительности
        /// </summary>
        private void numericUpDown_BeginHours_ValueChanged(object sender, EventArgs e)
        {
            SetEndDate();
        }

        /// <summary>
        /// Обработчик события изменения минуты начала обслуживания,
        /// который осуществляет корректировку допустимой даты окончания обслуживания и расчет длительности
        /// </summary>
        private void numericUpDown_BeginMinutes_ValueChanged(object sender, EventArgs e)
        {
            SetEndDate();
        }

        /// <summary>
        /// Обработчик события изменения даты окончания обслуживания,
        /// который осуществляет корректировку допустимой даты окончания обслуживания и расчет длительности
        /// </summary>
        private void dateTimePicker_End_ValueChanged(object sender, EventArgs e)
        {
            SetEndDate();
        }

        /// <summary>
        /// Обработчик события изменения часа окончания обслуживания,
        /// который осуществляет корректировку допустимой даты окончания обслуживания и расчет длительности
        /// </summary>
        private void numericUpDown_EndHours_ValueChanged(object sender, EventArgs e)
        {
            SetEndDate();
        }

        /// <summary>
        /// Обработчик события изменения минуты окончания обслуживания,
        /// который осуществляет корректировку допустимой даты окончания обслуживания и расчет длительности
        /// </summary>
        private void numericUpDown_EndMinutes_ValueChanged(object sender, EventArgs e)
        {
            SetEndDate();
        }

        /// <summary>
        /// Обработчик события изменения индекса формата отображения длительности обслуживания
        /// </summary>
        private void comboBox_DurationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox_Duration.Text))
                return;

            double result = 0;
            var tryParse = double.TryParse(textBox_Duration.Text, out result);

            switch (comboBox_DurationType.SelectedIndex)
            {
                case 0:
                    if (tryParse)
                        textBox_Duration.Text = (result * 60).ToString();
                    else
                    {
                        string[] strings = textBox_Duration.Text.Split(':');
                        var hours = int.Parse(strings[0]);
                        var minutes = int.Parse(strings[1]);
                        var timeSpan = new TimeSpan(hours, minutes, 0);
                        textBox_Duration.Text = textBox_Duration.Text = timeSpan.TotalMinutes.ToString();
                    }
                    break;
                case 1:
                    if (tryParse)
                        textBox_Duration.Text = TimeSpan.FromHours(TimeSpan.FromMinutes(result).TotalHours)
                            .ToString(@"hh\:mm");
                    break;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который выполняет сохранение изменений
        /// </summary>
        private async void btn_OK_Click(object sender, EventArgs e)
        {
            if (_beginDateTime >= _endDateTime)
            {
                MessageBox.Show("Конечная дата должна быть больше начальной!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (_beginDateTime <= DateTime.Now)
            {
                MessageBox.Show("Начальная дата должна быть больше текущей!", "Ошибка", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                bool checkResult = false;

                switch (_scheduleEditType)
                {
                    case ScheduleEditType.Server:
                        checkResult = await _ctx.CheckMaintenanceDateRange(
                            _serverMaintenanceShedule?.Id ?? -1,
                            _selectedServer?.Id ?? _serverMaintenanceShedule.ServerID,
                            true,
                            _beginDateTime, 
                            _endDateTime);
                        break;
                    case ScheduleEditType.SAN:
                        checkResult = await _ctx.CheckMaintenanceDateRange(
                            _sanMaintenanceShedule?.Id ?? -1,
                            _selectedSan?.Id ?? _sanMaintenanceShedule.SAN_Id,
                            false,
                            _beginDateTime,
                            _endDateTime);
                        break;
                }

                if (checkResult)
                {
                    MessageBox.Show(
                        "Уже существуют запланнированые позиции обслуживания в указанном временном промежутке!" +
                        "\nПозиция не будет сохранена!", "Предупреждение", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при проверке уже запланированных позиций обсулживания в установленном диапазоне дат!", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (_formMode == FormMode.Add)
            {
                try
                {
                    switch (_scheduleEditType)
                    {
                        case ScheduleEditType.Server:
                            await _ctx.AddNewServerSchedulePosition(new Server_MaintenanceShedule
                            {
                                Server = _selectedServer,
                                BeginPeriod = _beginDateTime,
                                EndPeriod = _endDateTime
                            });
                            break;
                        case ScheduleEditType.SAN:
                            await _ctx.AddNewSANSchedulePosition(new SAN_MaintenanceShedule
                            {
                                SAN = _selectedSan,
                                BeginPeriod = _beginDateTime,
                                EndPeriod = _endDateTime
                            });
                            break;
                    }

                    MessageBox.Show("Новая позиция в расписании обслуживания успешно создана!", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch
                {
                    MessageBox.Show("Не удалось сохранить новую позицию в расписании обслуживания!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                try
                {
                    switch (_scheduleEditType)
                    {
                        case ScheduleEditType.Server:
                            _serverMaintenanceShedule.BeginPeriod = _beginDateTime;
                            _serverMaintenanceShedule.EndPeriod = _endDateTime;
                            await _ctx.EditServerSchedulePosition(_serverMaintenanceShedule);
                            break;
                        case ScheduleEditType.SAN:
                            _sanMaintenanceShedule.BeginPeriod = _beginDateTime;
                            _sanMaintenanceShedule.EndPeriod = _endDateTime;
                            await _ctx.EditSANSchedulePosition(_sanMaintenanceShedule);
                            break;
                    }

                    MessageBox.Show("Изменения в позиции расписания обслуживания успешно сохранены!", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                catch
                {
                    MessageBox.Show("Не удалось сохранить изменения в позиции расписания обслуживания!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену создания / редактирования позиции расписания обслуживания, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Обработчик события закрытия формы
        /// </summary>
        private void EditScheduleItem_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DialogResult != DialogResult.OK)
            {
                var result = MessageBox.Show("Изменения не будут сохранены! Продолжить?", "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if (result == DialogResult.No)
                    e.Cancel = true;
            }
        }
    }
}
