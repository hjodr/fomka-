using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fomka_web.Helpers
{

    public static class CookieExtractor
    {
        public static LoginInfo ExctractLoginInfo(this HttpCookieCollection cookies)
            => new LoginInfo()
            {
                CustomerId = cookies?["custId"]?.Value,
                Username = cookies?["user"]?.Value,
                Password = cookies?["pass"]?.Value
            };
    }

    public class LoginInfo
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}