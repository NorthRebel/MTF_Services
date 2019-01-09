using System.Collections.Generic;
using System.Linq;

namespace MTF_Services.Model.Views
{
    public class ServicePartsInfo
    {
        /// <summary>
        /// Cуммарная минимальная стоимость виртуальной машины
        /// </summary>
        public decimal SummaryVMCost => CalcSummaryVMCost();
        /// <summary>
        /// Стоимость 1 ГГц процессора в час
        /// </summary>
        public decimal CPUCostByHour => CalcCPUCostByHour();

        /// <summary>
        /// Стоимость 1 ГБ оперативной памяти в час
        /// </summary>
        public decimal RAMCostByHour => CalcRAMCostByHour();

        /// <summary>
        /// Стоимость 1 ГБ накопителей сервера в час
        /// </summary>
        public decimal ServerStorageCostByHour => CalcServerStorageCostByHour();

        /// <summary>
        /// Стоимость 1 ГБ накопителей хранилища данных в час
        /// </summary>
        public decimal SanStorageCostByHour => CalcSanStorageCostByHour();

        /// <summary>
        /// Стоимость программного обеспечения в час
        /// </summary>
        public decimal SoftwareCostByHour => CalcSoftwareCostByHour();

        /// <summary>
        /// Стомость обслуживания сервера в час
        /// </summary>
        public decimal ServerMaintenanceByHour => CalcServerMaintenanceByHour();

        /// <summary>
        /// Стомость обслуживания хранилища данных в час
        /// </summary>
        public decimal SanMaintenanceByHour => CalcSanMaintenanceByHour();

        /// <summary>
        /// Стоимость обслуживания оборудования в час
        /// </summary>
        public decimal TotalMaintenanceByHour => CalcTotalMaintenanceByHour();

        #region Рассчет стоимости единицы ресурсов в час

        /// <summary>
        /// Расчет суммарной минимальной стоимости виртуальной машины
        /// </summary>
        private decimal CalcSummaryVMCost()
        {
            return CPUCostByHour + RAMCostByHour + ServerStorageCostByHour + SanStorageCostByHour +
                   TotalMaintenanceByHour;
        }

        /// <summary>
        /// Расчет стоимости 1 ГГц процессора в час
        /// </summary>
        public decimal CalcCPUCostByHour()
        {
            try
            {
                decimal percentByTotalPrice = 1 / (ServerTotalPrice / CPUPrice);
                decimal fiveYearsCost = (ServerTotalPrice / (decimal)CPUFrequency) * percentByTotalPrice;
                return fiveYearsCost / HoursInFiveYears;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Расчет стоимости 1 ГБ оперативной памяти в час
        /// </summary>
        public decimal CalcRAMCostByHour()
        {
            try
            {
                decimal percentByTotalPrice = 1 / (ServerTotalPrice / RAMPrice);
                decimal fiveYearsCost = (ServerTotalPrice / (decimal)ServerRAMVolume) * percentByTotalPrice;
                return fiveYearsCost / HoursInFiveYears;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Расчет стоимости 1 ГБ накопителей сервера в час
        /// </summary>
        public decimal CalcServerStorageCostByHour()
        {
            try
            {
                decimal percentByTotalPrice = 1 / (ServerTotalPrice / ServerStoragePrice);
                decimal fiveYearsCost = (ServerTotalPrice / (decimal)ServerStorageVolume) * percentByTotalPrice;
                return fiveYearsCost / HoursInFiveYears;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Расчет стоимости 1 ГБ накопителей хранилища данных в час
        /// </summary>
        public decimal CalcSanStorageCostByHour()
        {
            try
            {
                decimal percentByTotalPrice = 1 / (SanTotalPrice / SanStoragePrice);
                decimal fiveYearsCost = (SanTotalPrice / (decimal)SanStorageVolume) * percentByTotalPrice;
                return fiveYearsCost / HoursInFiveYears;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Расчет стоимости программного обеспечения в час
        /// </summary>
        public decimal CalcSoftwareCostByHour()
        {
            try
            {
                decimal percentByTotalPrice = 1 / ((ServerTotalPrice + SanTotalPrice) / SoftwarePrice);
                decimal fiveYearsCost = SoftwarePrice * percentByTotalPrice;
                return fiveYearsCost / HoursInFiveYears;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Расчет стоимости обслуживания сервера в час
        /// </summary>
        private decimal CalcServerMaintenanceByHour()
        {
            try
            {
                decimal percentByTotalPrice = 1 / (ServerTotalPrice / ServerMaintenance);
                decimal fiveYearsCost = ServerMaintenance * percentByTotalPrice;
                return fiveYearsCost / HoursInFiveYears;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Расчет стоимости обслуживания хранилища данных в час
        /// </summary>
        private decimal CalcSanMaintenanceByHour()
        {
            try
            {
                decimal percentByTotalPrice = 1 / (SanTotalPrice / SanMaintenance);
                decimal fiveYearsCost = SanMaintenance * percentByTotalPrice;
                return fiveYearsCost / HoursInFiveYears;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Расчет общей стоимости обслуживания оборудования в час
        /// </summary>
        private decimal CalcTotalMaintenanceByHour()
        {
            return ServerMaintenanceByHour + SanMaintenanceByHour + SoftwareCostByHour;
        }

        #endregion

        #region Предварительные рассчеты

        /// <summary>
        /// Часов в пяти годах
        /// </summary>
        private const ushort HoursInFiveYears = 43800;

        /// <summary>
        /// Выбранный сервер
        /// </summary>
        public Server Server { get; set; }

        /// <summary>
        /// Выбранное хранилище данных
        /// </summary>
        public SAN San { get; set; }

        /// <summary>
        /// Редактируемый сервис
        /// </summary>
        public Service Service { get; set; }

        /// <summary>
        /// Выбранное программное обеспечение
        /// </summary>
        public List<SoftwareInfo> SoftwareInfo { get; set; }

        #region Сервер

        /// <summary>
        /// Частота процессора сервера
        /// </summary>
        public double CPUFrequency => Server?.CPU.Frequency * CPUCount ?? 0;

        /// <summary>
        /// Объем оперативной памяти сервера
        /// </summary>
        private double ServerRAMVolume => Server?.Server_RAM.Sum(sr => sr.RAM.Volume * sr.Count) ?? 0;

        /// <summary>
        /// Объем накопителей сервера
        /// </summary>
        private double ServerStorageVolume => Server?.Server_Storage.Sum(ss => ss.Strorage.Volume * ss.Count) ?? 0;

        /// <summary>
        /// Кол-во процессоров в платформе
        /// </summary>
        public byte CPUCount => Server?.Platform.CPUCount ?? 0;

        /// <summary>
        /// Исходная стоимость процессора
        /// </summary>
        private decimal CPUPrice => Server?.CPU.Price ?? 0;

        /// <summary>
        /// Исходная стоимость оперативной памяти
        /// </summary>
        private decimal RAMPrice => Server?.Server_RAM.Sum(r => r.RAM.Price * r.Count) ?? 0;

        /// <summary>
        /// Исходная стоимость накопителей севера
        /// </summary>
        private decimal ServerStoragePrice => Server?.Server_Storage.Sum(ss => ss.Strorage.Price * ss.Count) ?? 0;

        /// <summary>
        /// Исходная стоимость обслуживания сервера
        /// </summary>
        private decimal ServerMaintenance => Server.AnnualMaintenance ?? 0;

        /// <summary>
        /// Стоимость сервера
        /// </summary>
        private decimal ServerTotalPrice => Server.Price ?? 0;

        #endregion

        #region Хранилище данных

        /// <summary>
        /// Объем накопителей хранилища данных
        /// </summary>
        private decimal SanStorageVolume => San?.SAN_Storage.Sum(ss => ss.Strorage.Volume * ss.Count.Value) ?? 0;

        /// <summary>
        /// Исходная стоимость хранилища данных
        /// </summary>
        private decimal SanTotalPrice => San.Price;

        /// <summary>
        /// Исходная стоимость накопиетелей хранилища данных
        /// </summary>
        private decimal SanStoragePrice => San?.SAN_Storage.Sum(ss => ss.Strorage.Price * ss.Count.Value) ?? 0;

        /// <summary>
        /// Исходная стоимость обслуживания хранилища данных
        /// </summary>
        private decimal SanMaintenance => San?.AnnualMaintenance ?? 0;

        #endregion

        #region Программное обеспечение

        /// <summary>
        /// Исходная стоимость программного обеспечения
        /// </summary>
        public decimal SoftwarePrice => SoftwareInfo?.Sum(ss => ss.Cost) ?? 0;

        #endregion

        #endregion

        #region Рассчет стоимости ресурсов

        /// <summary>
        /// Указанная частота процессора
        /// </summary>
        public double Frequency { get; set; }

        /// <summary>
        /// Указанное количество оперативной памяти
        /// </summary>
        public double RAMVolume { get; set; }

        /// <summary>
        /// Указанный объем жесткого диска
        /// </summary>
        public double StorageVolume { get; set; }

        /// <summary>
        /// Расчет итоговой стоимости сервиса
        /// </summary>
        private decimal CalculateFinalPrice()
        {
            try
            {
                return ((decimal)Frequency * CPUCostByHour) + ((decimal)Frequency * RAMCostByHour) + ((decimal)RAMVolume * SanStorageCostByHour) +
                       SummaryVMCost;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Итоговая стоимость сервиса
        /// </summary>
        public decimal FinalPrice => CalculateFinalPrice();

        #endregion
    }
}
