using System;

namespace MTF_Services.Model.Views
{
    public class ServiceDetailInfo
    {
        public int ID { get; set; }
        public string PaasType { get; set; }
        public string ServiceType { get; set; }
        public DateTime? Create_Date { get; set; }
        public DateTime? Delete_Date { get; set; }
        public string ServiceState { get; set; }
        public decimal CostPerHour { get; set; }
        public int UserCount { get; set; }
    }
}
