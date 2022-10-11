using AutoCount.Data;
using AutoCountMiddleWare.Model;
using AutoCountMiddleWare.Services.Interface;
using Microsoft.Extensions.Options;
using System.Data;

namespace AutoCountMiddleWare.Services
{
    public class CreditorService : ICreditorService
    {
        private readonly AutoCountSettings _appSettings;
        private readonly ILoginService _loginService;

        public CreditorService(
            IOptions<AutoCountSettings> appSettings,
            ILoginService loginService)
        {
            _appSettings = appSettings.Value;
            _loginService = loginService;
        }

        public CreditorRecord GetCreditor(string accountNo)
        {
            try
            {
                var userSession = _loginService.AutoCountLogin();
                if (userSession != null)
                {
                    CreditorRecord creditRec = RecordUtils.GetCreditor(userSession.DBSetting, accountNo);
                    if (creditRec != null)
                    {
                        return creditRec;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetCreditorsCount()
        {
            try
            {
                var userSession = _loginService.AutoCountLogin();
                if (userSession != null)
                {
                    //Construct Sql Select string with selection of Item Groups
                    string sqlSelectItemInItemGroup = "SELECT Count(*) as count FROM Creditor WHERE IsActive='T'";

                    //Get Item Code List in a table from Sql Server
                    DataRow tblItemCode = userSession.DBSetting.GetFirstDataRow(sqlSelectItemInItemGroup, false);

                    return int.Parse(tblItemCode["count"].ToString());
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CreditorsResponseModel GetFilterCreditors(int pageno = 0, int numberOfRecordsPerPage = 50)
        {
            try
            {
                var userSession = _loginService.AutoCountLogin();
                if (userSession != null)
                {
                    //Construct Sql Select string with selection of Item Groups
                    string sqlSelectCreditorInFilter = "SELECT AccNo, CompanyName FROM Creditor WHERE IsActive='T' ORDER BY AccNo OFFSET " + pageno + " ROWS FETCH NEXT " + numberOfRecordsPerPage + " ROWS ONLY";

                    //Get AccNo List in a table from Sql Server
                    DataTable tblCreditors = userSession.DBSetting.GetDataTable(sqlSelectCreditorInFilter, false);

                    var allItems = new List<CreditorModel>();

                    foreach (DataRow itmRow in tblCreditors.Rows)
                    {
                        var newcreditorItems = new CreditorModel
                        {
                            AccNo = itmRow["AccNo"].ToString(),
                            CompanyName = itmRow["CompanyName"].ToString()
                        };

                        allItems.Add(newcreditorItems);
                    }

                    var response = new CreditorsResponseModel
                    {
                        PageNo = pageno,
                        ReturnItems = tblCreditors.Rows.Count,
                        StartIndex = pageno * numberOfRecordsPerPage + 1,
                        EndIndex = pageno * numberOfRecordsPerPage + tblCreditors.Rows.Count,
                        Creditors = allItems
                    };

                    return response;
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
