using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using MTF_Services.Model.Views;
using MTF_Services.WinForms.Data;
using MTF_Services.WinForms.Extentions;

namespace MTF_Services.WinForms.Forms
{
    /// <summary>
    /// Класс формы предварительного просмотра отчетов
    /// </summary>
    public partial class ReportingForm : Form
    {
        private readonly ServiceIdleParams _serviceIdleParams;
        private readonly BindingList<ServiceDetailInfo> _servicesDetailInfo;
        private readonly string _costColumnHeaderName;
        private readonly ServiceRequestItem _serviceRequestItem;
        private readonly List<SoftwareInfo> _softwaresInfo;

        /// <summary>
        /// Тип выбранного отчета
        /// </summary>
        private readonly ReportType _reportType;

        /// <summary>
        /// Объект для доступа к контекту данных.
        /// </summary>
        private readonly Context _ctx;

        /// <summary>
        /// Конструктор формы предварительного просмотра отчетов
        /// </summary>
        private ReportingForm(ReportType reportType)
        {
            _reportType = reportType;
            InitializeComponent();
            SubscribeMenuItems();

            _ctx = new Context();
        }

        /// <summary>
        /// Конструктор формы для предварительного просмотра заявки на предоставление сервиса
        /// </summary>
        /// <param name="serviceRequestItem">Выбранная заявка</param>
        /// <param name="softwaresInfo">Программное обеспечение сервиса</param>
        public ReportingForm(ServiceRequestItem serviceRequestItem, List<SoftwareInfo> softwaresInfo) : this(ReportType.ServiceRequest)
        {
            _serviceRequestItem = serviceRequestItem;
            _softwaresInfo = softwaresInfo;
        }

        /// <summary>
        /// Конструктор формы для предварительного просмотра стоимости предоставления сервисов
        /// </summary>
        /// <param name="servicesDetailInfo">Списко используемых сервисов</param>
        /// <param name="softwaresInfo"></param>
        public ReportingForm(BindingList<ServiceDetailInfo> servicesDetailInfo, string costColumnHeaderName) : this(ReportType.ServiceCost)
        {
            _servicesDetailInfo = servicesDetailInfo;
            _costColumnHeaderName = costColumnHeaderName;
        }

        /// <summary>
        /// Конструктор формы для предварительного просмотра информации о простое сервиса
        /// </summary>
        /// <param name="serviceIdleParams"></param>
        public ReportingForm(ServiceIdleParams serviceIdleParams) : this(ReportType.ServiceIdle)
        {
            _serviceIdleParams = serviceIdleParams;
        }

        /// <summary>
        /// Обработчик события выхода курсора из элемента главного меню,
        /// который очищает область в строке состояния.
        /// </summary>
        private void MenuItem_MouseLeave(object sender, EventArgs e)
        {
            tip_Label.Text = string.Empty;
        }

        /// <summary>
        /// Обработчик события наведения курсора на элемент главного меню,
        /// который выводит подсказку элемента в строку состояния.
        /// </summary>
        private void MenuItem_MouseEnter(object sender, EventArgs e)
        {
            tip_Label.Text = ((ToolStripItem)sender).ToolTipText;
        }

        /// <summary>
        /// Подписка элементов главного меню и панели элементов на события 
        /// наведения и выхода курсора для отображения подсказок в строке состояния. 
        /// </summary>
        private void SubscribeMenuItems()
        {
            SubScribeChildMenuItems(menuBar.Items);
        }

        /// <summary>
        /// Подписка дочерних элементов главного меню на события 
        /// наведения и выхода курсора для отображения подсказок в строке состояния. 
        /// </summary>
        /// <param name="col">Коллекция элементов главного меню</param>
        private void SubScribeChildMenuItems(ToolStripItemCollection col)
        {
            for (int i = 0; i < col.Count; i++)
            {
                var item = col[i] as ToolStripMenuItem;
                if (item != null)
                {
                    item.MouseEnter += MenuItem_MouseEnter;
                    item.MouseLeave += MenuItem_MouseLeave;
                    if (item.DropDownItems.Count > 0)
                        SubScribeChildMenuItems(item.DropDownItems);
                }
            }
        }

        /// <summary>
        /// Обработчик события загрузки формы
        /// </summary>
        private void ReportingForm_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();

            switch (_reportType)
            {
                case ReportType.ServiceRequest:
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "MTF_Services.WinForms.Reports.ServiceRequestReport.rdlc";
                    ServiceRequestItemBindingSource.DataSource = _serviceRequestItem;
                    SoftwareInfoBindingSource.DataSource = _softwaresInfo;
                    ReportDataSource ds_Request = new ReportDataSource("ds_Request",ServiceRequestItemBindingSource);
                    ReportDataSource ds_SoftwareInfo = new ReportDataSource("ds_SoftwareInfo", SoftwareInfoBindingSource);
                    reportViewer1.LocalReport.DataSources.Add(ds_Request);
                    reportViewer1.LocalReport.DataSources.Add(ds_SoftwareInfo);
                    break;
                case ReportType.ServiceCost:
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "MTF_Services.WinForms.Reports.ServiceCostReport.rdlc";
                    ServiceDetailInfoBindingSource.DataSource = _servicesDetailInfo;
                    ReportDataSource serviceCostDS = new ReportDataSource("ds_ServiceDetail", ServiceDetailInfoBindingSource);
                    reportViewer1.LocalReport.DataSources.Add(serviceCostDS);
                    break;
                case ReportType.ServiceIdle:
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "MTF_Services.WinForms.Reports.ServiceIdleReport.rdlc";
                    BindingSource idleBs = new BindingSource() {DataSource = _serviceIdleParams};
                    ReportDataSource serviceIdleDS = new ReportDataSource("ds_ServiceIdleParams", idleBs);
                    reportViewer1.LocalReport.DataSources.Add(serviceIdleDS);
                    break;
            }
            this.reportViewer1.RefreshReport();
        }
    }
}
