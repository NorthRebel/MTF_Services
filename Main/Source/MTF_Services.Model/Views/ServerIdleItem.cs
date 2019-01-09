namespace MTF_Services.Model.Views
{
    public class ServerIdleItem
    {
        public int ID { get; set; }
        public string Plarform { get; set; }
        public string CPU { get; set; }
        public int RAM { get; set; }
        public bool OnMaintenance { get; set; }
        public bool Active { get; set; }
        public int CompleteIdleCount { get; set; }
        public int SheduledIdleCount { get; set; }
    }
}
