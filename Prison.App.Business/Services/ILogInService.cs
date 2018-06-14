using Prison.App.Common.Entities.Account;

namespace Prison.App.Business.Services
{
    public interface ILogInService
    {
        LoginResult LogIn(string login, string password);
        void LogOut();

    }
}
