using System;

namespace MTF_Services.Model
{
    public partial class Server_MaintenanceShedule
    {
        public int Id { get; set; }

        public int ServerID { get; set; }

        public DateTime BeginPeriod { get; set; }

        public DateTime EndPeriod { get; set; }

        public virtual Server Server { get; set; }
    }
}
