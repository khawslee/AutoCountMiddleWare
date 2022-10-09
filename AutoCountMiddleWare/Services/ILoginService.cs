namespace AutoCountMiddleWare.Services
{
    public interface ILoginService
    {
        AutoCount.Authentication.UserSession AutoCountLogin();
    }
}
