using AutoCount.Controls.Editors;
using AutoCount.Data;
using AutoCountMiddleWare.Controllers;
using AutoCountMiddleWare.Model;
using AutoCountMiddleWare.Services.Interface;
using Microsoft.Extensions.Options;
using System.Data;

namespace AutoCountMiddleWare.Services
{
    public class StockService : IStockService
    {
        private readonly AutoCountSettings _appSettings;
        private readonly ILoginService _loginService;
        private readonly ILogger<StockService> _logger;

        public StockService(
            IOptions<AutoCountSettings> appSettings,
            ILoginService loginService,
            ILogger<StockService> logger)
        {
            _appSettings = appSettings.Value;
            _loginService = loginService;
            _logger = logger;
        }

        public StockItemsModel GetStockItem(string itemCode, string uom)
        {
            try
            {
                var userSession = _loginService.AutoCountLogin();
                if (userSession != null)
                {
                    var itemRec = RecordUtils.GetItem(userSession.DBSetting, itemCode, uom);
                    if (itemRec != null)
                    {
                        var singleItemsModel = new StockItemsModel
                        {
                            ItemCode = itemRec.ItemCode,
                            ItemGroup = itemRec.ItemGroup,
                            ItemType = itemRec.ItemType,
                            Description = itemRec.Description,
                            UOM = itemRec.UOM,
                            BarCode = itemRec.BarCode
                        };
                        return singleItemsModel;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        public int GetStockItemsCount()
        {
            try
            {
                var userSession = _loginService.AutoCountLogin();
                if (userSession != null)
                {
                    //Construct Sql Select string with selection of Item Groups
                    string sqlSelectItemInItemGroup = "SELECT Count(*) as count FROM Item WHERE IsActive='T'";

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

        public StockItemsResponseModel GetFilterStockItems(int pageno = 0, int numberOfRecordsPerPage = 50)
        {
            try
            {
                var userSession = _loginService.AutoCountLogin();
                if (userSession != null)
                {
                    //Construct Sql Select string with selection of Item Groups
                    string sqlSelectItemInFilter = "SELECT ItemCode FROM Item WHERE IsActive='T' ORDER BY ItemCode OFFSET " + pageno + " ROWS FETCH NEXT " + numberOfRecordsPerPage + " ROWS ONLY";

                    //Get Item Code List in a table from Sql Server
                    DataTable tblItemCode = userSession.DBSetting.GetDataTable(sqlSelectItemInFilter, false);

                    //Convert Table of ItemCode to an Array of string
                    string[] itemArray = tblItemCode.AsEnumerable().Select(r => r.Field<string>("ItemCode")).ToArray();

                    //Create ItemDataAccess object,
                    //and call LoadAllItem(string[]) method to filter ItemCode
                    AutoCount.Stock.Item.ItemDataAccess cmd = AutoCount.Stock.Item.ItemDataAccess.Create(userSession, userSession.DBSetting);
                    AutoCount.Stock.Item.ItemEntities items = cmd.LoadAllItem(itemArray);

                    //items.ItemTable consists of UOM in single table
                    var itmTable = items.ItemTable;

                    var allItems = new List<StockItemsModel>();

                    foreach (DataRow itmRow in itmTable.Rows)
                    {
                        var newStockItems = new StockItemsModel
                        {
                            ItemCode = itmRow["ItemCode"].ToString(),
                            ItemGroup = itmRow["ItemGroup"].ToString(),
                            ItemType = itmRow["ItemType"].ToString(),
                            Description = itmRow["Description"].ToString(),
                            UOM = itmRow["UOM"].ToString(),
                            BarCode = itmRow["BarCode"].ToString()
                        };

                        allItems.Add(newStockItems);
                    }

                    var response = new StockItemsResponseModel
                    {
                        PageNo = pageno,
                        ReturnItems = itemArray.Length,
                        StartIndex = pageno * numberOfRecordsPerPage + 1,
                        EndIndex = pageno * numberOfRecordsPerPage + itemArray.Length,
                        StockItems = allItems
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
