using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace fomka_web.Common
{
    public static class AppSettings
    {
        public static int AuthCookieExpirationTime
        {
            get
            {
                int time = 0;
                int.TryParse(WebConfigurationManager.AppSettings[nameof(AuthCookieExpirationTime)]?.ToString(), out time);
                return time == 0 ? AuthCookieExpirationTime : time;
            }
        }
    }
}