using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prison.App.Business.Services
{
    public interface ILogInService
    {
        void LogIn(string login, string password);
        void LogOut();

    }
}
