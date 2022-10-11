using AutoCount.Data;
using AutoCountMiddleWare.Model;

namespace AutoCountMiddleWare.Services.Interface
{
    public interface ICreditorService
    {
        CreditorRecord GetCreditor(string accountNo);
        int GetCreditorsCount();
        CreditorsResponseModel GetFilterCreditors(int pageno = 0, int numberOfRecordsPerPage = 50);
    }
}
