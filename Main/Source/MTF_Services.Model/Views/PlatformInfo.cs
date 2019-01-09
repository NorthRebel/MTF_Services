namespace MTF_Services.Model.Views
{
    public class PlatformInfo
    {
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public byte CPU_Count { get; set; }
        public int RamVolume_Max { get; set; }
        public byte RamSocketCount { get; set; }
        public decimal Price { get; set; }
        public string CPUSocket { get; set; }
        public string RAMType { get; set; }
    }
}
