using System;

namespace MTF_Services.Model.Views
{
    public class IdleItem
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string PaasName { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Duration { get; set; }
        public string Cost { get; set; }
    }
}
