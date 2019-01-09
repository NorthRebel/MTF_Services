using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.SysAdmin.Infrastructure.Common
{
    /// <summary>
    /// Класс формы отбора конфигураций хранилищ данных
    /// </summary>
    public partial class SAN_ConditionConstructorForm : Form
    {
        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Флаг активации привязки данных после ее приоставновки в источнике данных производителей
        /// </summary>
        private bool _afterManufacturerSuspend;

        /// <summary>
        /// Выбранный производитель
        /// </summary>
        public Manufacturer SelectedManufacturer { get; set; }

        /// <summary>
        /// Список конфигураций хранилищ данных
        /// </summary>
        public BindingList<SAN_Info> SansInfoMain { get; set; }

        /// <summary>
        /// Отобранный список конфигураций хранилищ данных
        /// </summary>
        public BindingList<SAN_Info> SansInfoToShow { get; set; }

        /// <summary>
        /// Конструктор формы отбора конфигураций хранилищ данных
        /// </summary>
        public SAN_ConditionConstructorForm(BindingList<SAN_Info> sansInfoMain)
        {
            InitializeComponent();
            btn_Cancel.Image = new Bitmap(Resources.no, 20, 20);
            btn_OK.Image = new Bitmap(Resources.camera_test, 20, 20);

            _ctx = new Context();
            SansInfoMain = sansInfoMain;
            InitBindings();
            pictureBox1_Click(null, EventArgs.Empty);
            pictureBox2_Click(null, EventArgs.Empty);
        }

        /// <summary>
        /// Инициализация привязок
        /// </summary>
        private void InitBindings()
        {
            manufacturerBindingSource.DataSource = _ctx.GetManufacturers();
            comboBox2.Items.AddRange(SansInfoMain.Select(x => x.Model).Distinct().ToArray());
        }

        /// <summary>
        /// Обработчик события изменения текущего элемента в источнике данных уровней производителей
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

            if (manufacturerBindingSource.Position > -1)
            {
                var manufacturer = manufacturerBindingSource.Current as Manufacturer;
                if (manufacturer != null)
                {
                    SelectedManufacturer = manufacturer;
                    lbl_SelectedConfigsStatus.Text = $"Отобрано конфигураций: {RunSelection()}";
                }
            }
        }

        /// <summary>
        /// Выполнение отбора конфигураций хранилищ данных по условию
        /// </summary>
        private int RunSelection()
        {
            List<SAN_Info> selectedSan = null;

            try
            {
                if (SelectedManufacturer != null && comboBox2.SelectedIndex > -1)
                    selectedSan = SansInfoMain.Where(si =>
                            si.Manufacturer.Equals(SelectedManufacturer.Name) && si.Model.Equals(comboBox2.Items[comboBox2.SelectedIndex]))
                        .ToList();
                else if (SelectedManufacturer != null)
                    selectedSan = SansInfoMain.Where(si => si.Manufacturer.Equals(SelectedManufacturer.Name)).ToList();
                else if (comboBox2.SelectedIndex > -1)
                    selectedSan = SansInfoMain.Where(si => si.Model.Equals(comboBox2.Items[comboBox2.SelectedIndex])).ToList();
                else
                    selectedSan = SansInfoMain.ToList();

                SansInfoToShow = new BindingList<SAN_Info>(selectedSan);
                return SansInfoToShow.Count;
            }
            catch
            {
                SansInfoToShow = null;
                return 0;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет отмену выбора производителя
        /// </summary>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            SelectedManufacturer = null;
            manufacturerBindingSource.SuspendBinding();
            lbl_SelectedConfigsStatus.Text = $"Отобрано конфигураций: {RunSelection()}";
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет отмену выбора модели
        /// </summary>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            lbl_SelectedConfigsStatus.Text = $"Отобрано конфигураций: {RunSelection()}";
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену совершенного отбора записей
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
        /// Обработчик события изменения текущего индекса в списке моделей
        /// </summary>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex > -1)
                lbl_SelectedConfigsStatus.Text = $"Отобрано конфигураций: {RunSelection()}";
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на графический объект, 
        /// который осуществляет подтверждение совершенного отбора конфигураций хранилищ данных
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (SansInfoToShow != null && SansInfoToShow.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
                MessageBox.Show("Не найдены конфигурации хранилищ данных по указанному условию!", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
