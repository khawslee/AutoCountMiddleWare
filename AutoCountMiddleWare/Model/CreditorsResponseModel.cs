namespace AutoCountMiddleWare.Model
{
    public class CreditorsResponseModel : PaginateResponseModel
    {
        public List<CreditorModel> Creditors { get; set; }
    }
}
