namespace AutoCountMiddleWare.Model
{
    public class StockItemsResponseModel : PaginateResponseModel
    {
        public List<StockItemsModel> StockItems { get; set; }
    }
}
