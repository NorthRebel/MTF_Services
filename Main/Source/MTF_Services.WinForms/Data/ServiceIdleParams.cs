using System;

namespace MTF_Services.WinForms.Data
{
    public class ServiceIdleParams
    {
        public int Id { get; set; }
        public string Service { get; set; }
        public string Platform { get; set; }
        public DateTime Begin { get; set; }
        public DateTime End { get; set; }
        public string DurationType { get; set; }
        public string DurationValue { get; set; }
        public string IdleType { get; set; }
        public string IdleReason { get; set; }
        public int DisabledEmployees { get; set; }
        public int UsedEmployees { get; set; }
        public decimal ServerCost { get; set; }
        public decimal LostCost { get; set; }
        public decimal TotalCost { get; set; }
    }
}
