using AutoCountMiddleWare.Model;
using AutoCountMiddleWare.Services;
using AutoCountMiddleWare.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AutoCountMiddleWare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly ILogger<PurchaseController> _logger;
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(
            ILogger<PurchaseController> logger,
            IPurchaseService purchaseService)
        {
            _logger = logger;
            _purchaseService = purchaseService;
        }

        [HttpPost("CreateGRNote")]
        public ActionResult<int> CreateGRNote(GRNoteRequestModel grnoteRequest)
        {
            try
            {
                return _purchaseService.CreateGRNote(grnoteRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetPurchaseOrder")]
        public ActionResult<POResponseModel> GetPurchaseOrder(string docNo)
        {
            try
            {
                _logger.LogDebug(">>>GetPurchaseOrder");
                return _purchaseService.GetPurchaseOrder(docNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetPurchaseOrderList")]
        public ActionResult<List<string>> GetPurchaseOrderList(string docNo)
        {
            try
            {
                _logger.LogDebug(">>>GetPurchaseOrderList");
                return _purchaseService.GetPurchaseOrderList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
