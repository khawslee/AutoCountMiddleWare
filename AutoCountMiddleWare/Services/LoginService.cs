using AutoCount.Authentication;
using AutoCountMiddleWare.Controllers;
using AutoCountMiddleWare.Model;
using AutoCountMiddleWare.Services.Interface;
using Microsoft.Extensions.Options;

namespace AutoCountMiddleWare.Services
{
    public class LoginService : ILoginService
    {
        private readonly AutoCountSettings _appSettings;
        private readonly ILogger<StockController> _logger;

        public LoginService(
            IOptions<AutoCountSettings> appSettings,
            ILogger<StockController> logger)
        {
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public AutoCount.Authentication.UserSession AutoCountLogin()
        {
            if (GlobalUtils.userSession == null)
            {
                string serverName = _appSettings.ServerName;
                string dbName = _appSettings.DatabaseName;
                string saPassword = _appSettings.SaPassword;
                string userName = _appSettings.AutoCountUser;
                string passWord = _appSettings.AutoCountPass;

                var newSession = GetUserSession(serverName, dbName, saPassword);
                newSession.Login(userName, passWord);
                GlobalUtils.userSession = newSession;
            }
            return GlobalUtils.userSession;
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
                _logger.LogError(">>>Error GetUserSession: " + ex.Message);
                throw ex;
            }
        }

    }
}
