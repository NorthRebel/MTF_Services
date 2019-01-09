using System;

namespace MTF_Services.Model.Views
{
    public class ScheduleItem
    {
        public int Id { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Duration { get; set; }
    }
}
