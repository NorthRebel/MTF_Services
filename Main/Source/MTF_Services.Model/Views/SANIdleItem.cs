namespace MTF_Services.Model.Views
{
    public class SANIdleItem
    {
        public int ID { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int StorageCount { get; set; }
        public int StorageVolume { get; set; }
        public bool OnMaintenance { get; set; }
        public bool Active { get; set; }
        public int CompleteIdleCount { get; set; }
        public int SheduledIdleCount { get; set; }
    }
}
