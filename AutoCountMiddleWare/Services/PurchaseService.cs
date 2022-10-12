using AutoCountMiddleWare.Constants;
using AutoCountMiddleWare.Model;
using AutoCountMiddleWare.Services.Interface;
using Microsoft.Extensions.Options;
using System.Data;

namespace AutoCountMiddleWare.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly AutoCountSettings _appSettings;
        private readonly ILoginService _loginService;

        public PurchaseService(
            IOptions<AutoCountSettings> appSettings,
            ILoginService loginService)
        {
            _appSettings = appSettings.Value;
            _loginService = loginService;
        }

        public int CreateGRNote(GRNoteRequestModel grnoteRequest)
        {
            try
            {
                var userSession = _loginService.AutoCountLogin();
                if (userSession != null)
                {
                    var cmd =
        AutoCount.Invoicing.Purchase.GoodsReceivedNote.GoodsReceivedNoteCommand.Create(userSession, userSession.DBSetting);
                    var doc = cmd.AddNew();
                    AutoCount.Invoicing.Purchase.GoodsReceivedNote.GoodsReceivedNoteDetail dtl;

                    doc.DocDate = DateTime.Today.Date;      //get only date
                    doc.CreditorCode = grnoteRequest.CreditorCode;
                    doc.SupplierDONo = grnoteRequest.SupplierDO;
                    doc.PurchaseLocation = grnoteRequest.Location;
                    doc.Description = "Good Receive note - " + doc.DocDate.ToString();

                    foreach(var item in grnoteRequest.Items)
                    {
                        dtl = doc.AddDetail();
                        dtl.ItemCode = item.ItemCode;
                        dtl.Qty = item.Qty;
                    }

                    doc.Save();

                    return 0;
                }
                return Error.ERR_GRNOTE_CREATE;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
