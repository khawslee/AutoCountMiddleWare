using AutoCountMiddleWare.Model;
using Microsoft.AspNetCore.Mvc;

namespace AutoCountMiddleWare.Services.Interface
{
    public interface IPurchaseService
    {
        int CreateGRNote(POGRResponseModel grnoteRequest);
        POGRResponseModel GetPurchaseOrder(string docNo);
        int GRNFullTransferFromPO(POGRResponseModel grnoteRequest);
    }
}
