namespace AutoCountMiddleWare.Model
{
    public class StockItemsModel
    {
        public string ItemCode { get; set; }
        public string ItemGroup { get; set; }
        public string ItemType { get; set; }
        public string Description { get; set; }
        public string UOM { get; set; }
        public string BarCode { get; set; }

        public StockItemsModel()
        {
            ItemCode = string.Empty;
            ItemGroup = string.Empty;
            ItemType = string.Empty;
            Description = string.Empty;
            UOM = string.Empty;
            BarCode = string.Empty;
        }
    }
}
