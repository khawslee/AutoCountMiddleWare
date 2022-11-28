using AutoCountMiddleWare.Model;

namespace AutoCountMiddleWare.Services.Interface
{
    public interface IPurchaseService
    {
        int CreateGRNote(GRNoteRequestModel grnoteRequest);
        POResponseModel GetPurchaseOrder(string docNo);
    }
}
