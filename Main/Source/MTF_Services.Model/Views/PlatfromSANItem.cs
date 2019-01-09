using System.Linq;

namespace MTF_Services.Model.Views
{
    public class PlatfromSANItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Конструктор класса описания хранилища данных платформы
        /// </summary>
        public PlatfromSANItem(SAN san)
        {
            ID = san.Id;
            Name = $"{san.Manufacturer.Name} {san.Model}; {san.SAN_Storage.Sum(ss => ss.Strorage.Volume)} ГБ";
        }
    }
}
