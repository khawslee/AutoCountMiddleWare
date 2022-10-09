namespace AutoCountMiddleWare.Model
{
    public class StockItemsResponseModel
    {
        public int PageNo { get; set; }
        public int ReturnItems { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public List<StockItemsModel> StockItems { get; set; }
    }
}
