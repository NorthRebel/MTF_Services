namespace MTF_Services.Model.Views
{
    public class ServerInfo
    {
        public int Id { get; set; }
        public string Platform { get; set; }
        public int CpuCount { get; set; }
        public string CPU { get; set; }
        public int RamSlotCount { get; set; }
        public int RamVolume { get; set; }
        public int StorageCount { get; set; }
        public int StorageVolume { get; set; }
        public int PaasCount { get; set; }
        public int ServiceCount { get; set; }
    }
}
