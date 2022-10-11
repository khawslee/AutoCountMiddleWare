using AutoCount.Data;
using AutoCountMiddleWare.Model;
using AutoCountMiddleWare.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AutoCountMiddleWare.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreditorController
    {
        private readonly ILogger<CreditorController> _logger;
        private readonly ICreditorService _creditorService;

        public CreditorController(
            ILogger<CreditorController> logger,
            ICreditorService creditorService)
        {
            _logger = logger;
            _creditorService = creditorService;
        }

        [HttpGet]
        public ActionResult<CreditorRecord> GetCreditor(string accountNo)
        {
            try
            {
                return _creditorService.GetCreditor(accountNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetCreditorsCount")]
        public ActionResult<int> GetCreditorsCount()
        {
            try
            {
                return _creditorService.GetCreditorsCount();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetFilterCreditors")]
        public ActionResult<CreditorsResponseModel> GetFilterCreditors(int pageno = 0, int numberOfRecordsPerPage = 50)
        {
            try
            {
                return _creditorService.GetFilterCreditors(pageno, numberOfRecordsPerPage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
