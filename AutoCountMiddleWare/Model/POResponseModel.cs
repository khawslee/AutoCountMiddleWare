namespace AutoCountMiddleWare.Model
{
    public class POResponseModel
    {
        public string CreditorCode { get; set; }
        public string CreditorName { get; set; }
        public string SupplierDO { get; set; }
        public string Location { get; set; }
        public List<POItemsModel> Items { get; set; }
    }
}
