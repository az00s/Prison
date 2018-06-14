﻿using Newtonsoft.Json;
using Prison.App.Business.Providers;
using Prison.App.Common.Entities;
using Prison.App.Common.Entities.Account;
using Prison.App.Common.Helpers;
using Prison.App.Common.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Security;

namespace Prison.App.Business.Services
{
    public class LogInService:ILogInService
    {
        private IUserProvider _usr;
        private ILogger _log;

        public LogInService(ILogger log,IUserProvider prov)
        {
            ArgumentHelper.ThrowExceptionIfNull(log, "ILogger");
            ArgumentHelper.ThrowExceptionIfNull(prov, "IUserProvider");
            _log = log;
            _usr = prov;
        }
        public LoginResult LogIn(string login,string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return LoginResult.Failure;
            }

            if (IsValidUser(login))
            {
                if (_usr.GetUserPasswordByLogin(login).Equals(password))
                {
                    var User = GetUser(login);

                    if (User != null)
                    {
                        var UserData = JsonConvert.SerializeObject(User);
                        var authTicket = new FormsAuthenticationTicket(1, login, DateTime.Now, DateTime.Now.AddMinutes(15), false, UserData);
                        var encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                    return LoginResult.Success;
                }
                else
                {
                    _log.Warn($"Invalid Password for '{login}'!");
                    return LoginResult.InvalidPassword;
                }

            }
            return LoginResult.UserNotFound;
        }
        
        public void LogOut()
        {
            FormsAuthentication.SignOut();
        }

        private bool IsValidUser(string login)
        {

            if (_usr.GetAllLogins().Contains(login))
            {
                return true;
            }
            else
            {
                _log.Warn($"Login '{login}' not fount!");
                return false;
            }
        }

        private User GetUser(string login)
        {
            var User = _usr.GetUserByLogin(login);
            if (User == null)
            {
                _log.Warn($"User '{login}' not found!");
            }
            return User;
        }
    }
}
