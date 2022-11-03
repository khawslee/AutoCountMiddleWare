using AutoCount.Data;
using AutoCountMiddleWare.Model;
using AutoCountMiddleWare.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AutoCountMiddleWare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly ILogger<StockController> _logger;
        private readonly IStockService _stockService;

        public StockController(
            ILogger<StockController> logger,
            IStockService stockService)
        {
            _logger = logger;
            _stockService = stockService;
        }

        [HttpGet]
        public ActionResult<StockItemsModel> GetStockItem(string itemCode, string uom)
        {
            try
            {
                return _stockService.GetStockItem(itemCode, uom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetStockItemsCount")]
        public ActionResult<int> GetStockItemsCount()
        {
            try
            {
                _logger.LogDebug(">>>GetStockItemsCount");
                return _stockService.GetStockItemsCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetFilterStockItems")]
        public ActionResult<StockItemsResponseModel> GetFilterStockItems(int pageno = 0, int numberOfRecordsPerPage = 50)
        {
            try
            {
                return _stockService.GetFilterStockItems(pageno, numberOfRecordsPerPage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
