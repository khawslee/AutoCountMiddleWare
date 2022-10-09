using AutoCountMiddleWare.Model;
using Microsoft.Extensions.Options;

namespace AutoCountMiddleWare.Services
{
    public class LoginService : ILoginService
    {
        private readonly AutoCountSettings _appSettings;

        public LoginService(
            IOptions<AutoCountSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AutoCount.Authentication.UserSession AutoCountLogin()
        {
            string serverName = _appSettings.ServerName;
            string dbName = _appSettings.DatabaseName;
            string saPassword = _appSettings.SaPassword;
            string userName = _appSettings.AutoCountUser;
            string passWord = _appSettings.AutoCountPass;

            return GetUserSession(serverName, dbName, saPassword);
        }

        private AutoCount.Authentication.UserSession GetUserSession(string serverName, string dbName, string saPassword)
        {
            try
            {
                if (string.IsNullOrEmpty(saPassword))
                    return new AutoCount.Authentication.UserSession(new AutoCount.Data.DBSetting(AutoCount.Data.DBServerType.SQL2000, serverName, dbName));
                else
                    return new AutoCount.Authentication.UserSession(new AutoCount.Data.DBSetting(AutoCount.Data.DBServerType.SQL2000, serverName,
                        AutoCount.Const.AppConst.DefaultUserName, saPassword, dbName));
            }
            catch (AutoCount.AppException ex)
            {
                Console.Write(">>>Error GetUserSession: " + ex.Message);
                return null;
            }
        }

    }
}
