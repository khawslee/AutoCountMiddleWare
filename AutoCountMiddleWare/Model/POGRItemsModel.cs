namespace AutoCountMiddleWare.Model
{
    public class POGRItemsModel : StockItemsModel
    {
        public decimal POQty { get; set; }
        public decimal ReceiveQty { get; set; }

        public POGRItemsModel()
        {
            POQty = 0;
            ReceiveQty = 0;
        }
    }
}
