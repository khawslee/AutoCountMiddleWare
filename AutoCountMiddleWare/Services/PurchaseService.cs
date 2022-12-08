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

        public int GRNFullTransferFromPO(POGRResponseModel grnoteRequest)
        {
            try
            {
                var userSession = _loginService.AutoCountLogin();
                if (userSession != null)
                {
                    var cmd =
        AutoCount.Invoicing.Purchase.GoodsReceivedNote.GoodsReceivedNoteCommand.Create(userSession, userSession.DBSetting);
                    var doc = cmd.AddNew();

                    doc.CreditorCode = grnoteRequest.CreditorCode;
                    doc.DocDate = DateTime.Today.Date;
                    doc.SupplierDONo = grnoteRequest.SupplierDO;
                    //Purchase Order numbers to be transferred into Goods Received Note
                    string[] poDocNos = { grnoteRequest.PONo };

                    doc.FullTransfer(poDocNos, AutoCount.Invoicing.Purchase.TransferFrom.PurchaseOrder, AutoCount.Invoicing.FullTransferOption.FullDetails);

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

        public int GRNPartialTransferFromPO(POGRResponseModel grnoteRequest)
        {
            try
            {
                var userSession = _loginService.AutoCountLogin();
                if (userSession != null)
                {
                    var cmd =
        AutoCount.Invoicing.Purchase.GoodsReceivedNote.GoodsReceivedNoteCommand.Create(userSession, userSession.DBSetting);
                    var doc = cmd.AddNew();

                    doc.CreditorCode = grnoteRequest.CreditorCode;
                    doc.SupplierDONo = grnoteRequest.SupplierDO;
                    doc.DocDate = DateTime.Today.Date;

                    //Transfer one line of item from PO, if more than one line, write a loop
                    string poDocNo = grnoteRequest.PONo;

                    foreach(var gr in grnoteRequest.Items)
                    {                        
                        string itemCode = gr.ItemCode;
                        string uom = gr.UOM;
                        decimal qtyToTransfer = gr.ReceiveQty;
                        decimal focQtyToTrasnfer = 0;
                        //Using Partial Transfer
                        doc.PartialTransfer(AutoCount.Invoicing.Purchase.TransferFrom.PurchaseOrder,
                            poDocNo, itemCode, uom, qtyToTransfer, focQtyToTrasnfer);
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
