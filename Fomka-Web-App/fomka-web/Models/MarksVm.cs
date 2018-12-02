using fomka_web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static fomka_web.Attributes.RoleAuthorizeAttribute;

namespace fomka_web.Models
{
    public class MarksVm
    {
        public AppUserType Role;
        public User User { get; set; }
        public List<Task> Tasks { get; set; }
        public List<User> Students { get; set; }
    }
}