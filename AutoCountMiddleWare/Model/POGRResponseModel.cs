namespace AutoCountMiddleWare.Model
{
    public class POGRResponseModel
    {
        public string PONo { get; set; }
        public string CreditorCode { get; set; }
        public string CreditorName { get; set; }
        public string SupplierDO { get; set; }
        public string Location { get; set; }
        public List<POGRItemsModel> Items { get; set; }

        public POGRResponseModel()
        {
            PONo = string.Empty;
            CreditorCode = string.Empty;
            CreditorName = string.Empty;
            SupplierDO = string.Empty;
            Location = string.Empty;
            Items = new List<POGRItemsModel>();
        }
    }
}
