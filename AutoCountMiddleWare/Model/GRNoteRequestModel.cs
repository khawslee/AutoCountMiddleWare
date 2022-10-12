namespace AutoCountMiddleWare.Model
{
    public class GRNoteRequestModel
    {
        public string CreditorCode { get; set; }
        public string SupplierDO { get; set; }
        public string Location { get; set; }
        public List<ItemModel> Items { get; set; }
    }
}
