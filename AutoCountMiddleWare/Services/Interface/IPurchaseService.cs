using AutoCountMiddleWare.Model;

namespace AutoCountMiddleWare.Services.Interface
{
    public interface IPurchaseService
    {
        int CreateGRNote(POGRResponseModel grnoteRequest);
        POGRResponseModel GetPurchaseOrder(string docNo);
    }
}
