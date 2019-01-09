using System.Linq;

namespace MTF_Services.Model.Views
{
    public class PlatformServerItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Конструктор класса описания сервера платформы
        /// </summary>
        public PlatformServerItem(Server server)
        {
            ID = server.Id;
            Name = $"{server.Platform.Manufacturer.Name} {server.Platform.Model}; " +
                   $"{server.CPU.Model} {server.CPU.Manufacturer.Name} {server.CPU.Frequency} Ггц х {server.Platform.CPUCount}; " +
                   $"{server.Server_RAM.Sum(sr => sr.RAM.Volume * sr.Count)} ГБ";
        }
    }
}
