using AutoCount.GL;
using AutoCountMiddleWare.Constants;
using AutoCountMiddleWare.Model;
using AutoCountMiddleWare.Services.Interface;
using Microsoft.Extensions.Options;
using System.Data;
using System.Reflection;

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

        public int CreateGRNote(POGRResponseModel grnoteRequest)
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
                        dtl.Qty = item.ReceiveQty;
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

        public POGRResponseModel? GetPurchaseOrder(string docNo)
        {
            try
            {
                var userSession = _loginService.AutoCountLogin();
                if (userSession != null)
                {
                    var cmd = AutoCount.Invoicing.Purchase.PurchaseOrder.PurchaseOrderCommand.Create(userSession, userSession.DBSetting);
                    var doc = cmd.View(docNo);

                    if (doc != null)
                    {
                        var poResponse = new POGRResponseModel
                        {
                            PONo = docNo,
                            CreditorCode = doc.CreditorCode,
                            CreditorName = doc.CreditorName,
                            SupplierDO = "",
                            Location = doc.PurchaseLocation,
                            Items = new List<POGRItemsModel>()
                        };

                        var dtlTable = doc.DataTableDetail;

                        foreach (DataRow podtl in dtlTable.Rows)
                        {
                            string itemC = GlobalUtils.NullToStr(podtl["ItemCode"]);
                            if (!String.IsNullOrEmpty(itemC))
                            {
                                var newPOItem = new POGRItemsModel
                                {
                                    ItemCode = GlobalUtils.NullToStr(podtl["ItemCode"]),
                                    Description = GlobalUtils.NullToStr(podtl["Description"]),
                                    UOM = GlobalUtils.NullToStr(podtl["UOM"]),
                                    POQty = GlobalUtils.NullToDecimal(podtl["Qty"])
                                };
                                poResponse.Items.Add(newPOItem);
                            }
                        }

                        return poResponse;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
