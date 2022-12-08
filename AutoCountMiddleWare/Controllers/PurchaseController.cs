using AutoCountMiddleWare.Model;
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
        public ActionResult<int> CreateGRNote(POGRResponseModel grnoteRequest)
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
        public ActionResult<POGRResponseModel> GetPurchaseOrder(string docNo)
        {
            try
            {
                _logger.LogDebug(">>>GetPurchaseOrder: " + docNo);
                return _purchaseService.GetPurchaseOrder(docNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GRNFullTransferFromPO")]
        public ActionResult<int> GRNFullTransferFromPO(POGRResponseModel grnoteRequest)
        {
            try
            {
                _logger.LogDebug(">>>GRNFullTransferFromPO: " + grnoteRequest.PONo);
                return _purchaseService.GRNFullTransferFromPO(grnoteRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("GRNPartialTransferFromPO")]
        public ActionResult<int> GRNPartialTransferFromPO(POGRResponseModel grnoteRequest)
        {
            try
            {
                _logger.LogDebug(">>>GRNPartialTransferFromPO: " + grnoteRequest.PONo);
                return _purchaseService.GRNPartialTransferFromPO(grnoteRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
