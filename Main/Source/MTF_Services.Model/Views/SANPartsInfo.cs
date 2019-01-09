namespace MTF_Services.Model.Views
{
    public class SANPartsInfo
    {
        public decimal InitialPrice { get; set; }
        public decimal SelectedStoragePrice { get; set; }
        public decimal StorageSumPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Maintenance { get; set; }
        
        /// <summary>
        /// Обновление итоговой стоимости хранилища данных
        /// </summary>
        public void UpdateTotalPrice()
        {
            TotalPrice = InitialPrice + StorageSumPrice;
        }
    }
}
