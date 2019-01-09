using System;

namespace MTF_Services.Model.Views
{
    public class ServiceRequestItem
    {
        public int Id { get; set; }
        public string PaasTypeName { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal CostPerHour { get; set; }
    }
}
