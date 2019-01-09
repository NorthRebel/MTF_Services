using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MTF_Services.Model;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Properties;

namespace MTF_Services.WinForms.Forms.Director
{
    /// <summary>
    /// Класс формы офбработки заявки на предоставление сервиса
    /// </summary>
    public partial class ServiceRequestTreatment : Form
    {
        /// <summary>
        /// Текущий сервер
        /// </summary>
        private readonly Service _selectedService;

        private readonly ServiceRequestItem _selectedServiceRequest;

        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        public List<SoftwareInfo> InstalledSoftware { get; set; }

        /// <summary>
        /// Конструктор формы обработки  заявки на предоставление сервиса
        /// </summary>
        public ServiceRequestTreatment(Service selectedService, ServiceRequestItem selectedServiceRequest)
        {
            _selectedService = selectedService;
            _selectedServiceRequest = selectedServiceRequest;
            InitializeComponent();
            btn_OK.Image = new Bitmap(Resources.camera_test, new Size(20, 20));
            btn_Cancel.Image = new Bitmap(Resources.no, new Size(20, 20));
            btn_PrintReport.Image = new Bitmap(Resources.printers_and_faxes, new Size(30, 20));

            _ctx = new Context();
            UpdateTextFields();
            comboBox_PriceType.SelectedIndex = 0;
        }

        /// <summary>
        /// Обновление текстовых полей формы
        /// </summary>
        private void UpdateTextFields()
        {
            InstalledSoftware = new List<SoftwareInfo>();

            txt_Name.Text = _selectedService.ServiceType.Name;
            txt_Platform.Text = _selectedService.PaasType.Name;
            txt_CreateDate.Text = _selectedService.CreateDate.Value.ToString("d");
            txt_Price.Text = _selectedService.CostPerHour.Value.ToString();
            txt_CoreCount.Text = _selectedService.CoreCount.ToString();
            txt_RamValue.Text = _selectedService.RamCount.Value.ToString();
            txt_HddVolume.Text = _selectedService.HDDVolume.Value.ToString();

            foreach (var software in _selectedService.Software)
            {
                InstalledSoftware.Add(new SoftwareInfo
                {
                    Id = software.Id,
                    Software = software.Name,
                    SoftType = software.SoftType.Name,
                    Cost = software.Cost
                });
            }

            softwareInfoBindingSource.DataSource = InstalledSoftware;
            dg_Software.DataSource = softwareInfoBindingSource;
        }

        /// <summary>
        /// Обработчик события изменения индекса типа вывода стоимости
        /// </summary>
        private void comboBox_PriceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_selectedService == null)
                return;

            decimal initialPrice = _selectedService.CostPerHour.Value;

            switch (comboBox_PriceType.SelectedIndex)
            {
                case 0:
                    txt_Price.Text = initialPrice.ToString("##.00");
                    break;
                case 1:
                    txt_Price.Text = (initialPrice * 720).ToString("##.00");
                    break;
                case 2:
                    txt_Price.Text = (initialPrice * (720 * 3)).ToString("##.00");
                    break;
                case 3:
                    txt_Price.Text = (initialPrice * (720 * 12)).ToString("##.00");
                    break;
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит отмену заявки на создание сервиса, закрывая форму
        /// </summary>
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Заявка на создание сервиса будет отклонена! Продолжить?", "Потверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _ctx.CancelServiceRequest(_selectedService);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при отмене заявки!", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который производит подтверждение заявки на создание сервиса
        /// </summary>
        private void btn_OK_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Заявка на создание сервиса будет принята! Продолжить?", "Потверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _ctx.ConfirmServiceRequest(_selectedService);
                    Close();
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка при принятии заявки!", "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обработчик события нажатия клавиши мыши на кнопку, 
        /// который открывает диалоговое окно предварительного просмотра отчета заявки
        /// </summary>
        private void btn_PrintReport_Click(object sender, EventArgs e)
        {
            var reportingForm = new ReportingForm(_selectedServiceRequest, InstalledSoftware);
            reportingForm.ShowDialog();
        }
    }
}
