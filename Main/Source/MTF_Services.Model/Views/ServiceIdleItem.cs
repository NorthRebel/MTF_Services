namespace MTF_Services.Model.Views
{
    public class ServiceIdleItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PaasTypeName { get; set; }
        public string CurrentState { get; set; }
        public int UsersCount { get; set; }
        public int CompleteIdleCount { get; set; }
        public int SheduledIdleCount { get; set; }
    }
}
