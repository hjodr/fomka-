using fomka_web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fomka_web.Domain
{
    public class UserRepo
    {
        public LoginInfo Credentials { get; set; }

        public UserRepo(LoginInfo credentials)
        {
            Credentials = credentials;
        }
    }
}