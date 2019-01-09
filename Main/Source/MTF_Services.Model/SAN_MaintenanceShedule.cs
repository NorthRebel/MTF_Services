using System;

namespace MTF_Services.Model
{
    public partial class SAN_MaintenanceShedule
    {
        public int Id { get; set; }

        public int SAN_Id { get; set; }

        public DateTime BeginPeriod { get; set; }

        public DateTime EndPeriod { get; set; }

        public virtual SAN SAN { get; set; }
    }
}
