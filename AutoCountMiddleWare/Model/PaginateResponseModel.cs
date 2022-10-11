namespace AutoCountMiddleWare.Model
{
    public class PaginateResponseModel
    {
        public int PageNo { get; set; }
        public int ReturnItems { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
    }
}
