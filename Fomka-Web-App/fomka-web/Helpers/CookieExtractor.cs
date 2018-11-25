using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace fomka_web.Helpers
{

    public static class CookieExtractor
    {
        public static LoginInfo ExctractLoginInfo(this HttpCookieCollection cookies)
            => new LoginInfo()
            {
                UserId = Convert.ToInt32(cookies?["userId"]?.Value) ,
                Username = cookies?["user"]?.Value,
                Password = cookies?["pass"]?.Value
            };
    }

    public class LoginInfoWrapper
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginInfo : IPrincipal
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginInfo(string username)
        {
            this.Identity = new GenericIdentity(username);
        }

        public LoginInfo()
            :this("defname")
        {

        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return false;
        }

    }
}