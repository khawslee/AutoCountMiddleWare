using AutoCount.Data;
using AutoCountMiddleWare.Model;

namespace AutoCountMiddleWare.Services.Interface
{
    public interface IStockService
    {
        StockItemsModel GetStockItem(string itemCode, string uom);
        int GetStockItemsCount();
        StockItemsResponseModel GetFilterStockItems(int pageno = 0, int numberOfRecordsPerPage = 50);
    }
}
