namespace MTF_Services.Model.Views
{
    public class ConfigurationPartsInfo
    {
        public decimal PlatfromPrice { get; set; }
        public decimal OneCPUPrice { get; set; }
        public decimal CPUSumPrice { get; set; }
        public decimal SelectedRamPrice { get; set; }
        public decimal RAMSumPrice { get; set; }
        public decimal SelectedStoragePrice { get; set; }
        public decimal StorageSumPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Maintenance { get; set; }

        /// <summary>
        /// Обновление итоговой стоимости конфигурации сервера
        /// </summary>
        public void UpdateTotalPrice()
        {
            TotalPrice = PlatfromPrice + CPUSumPrice + RAMSumPrice + StorageSumPrice + SelectedStoragePrice;
        }
    }
}
