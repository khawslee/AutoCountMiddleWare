namespace AutoCountMiddleWare.Services.Interface
{
    public interface ILoginService
    {
        AutoCount.Authentication.UserSession AutoCountLogin();
    }
}
