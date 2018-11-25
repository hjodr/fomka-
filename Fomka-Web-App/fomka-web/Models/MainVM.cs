using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using fomka_web.DAL;
using static fomka_web.Attributes.RoleAuthorizeAttribute;

namespace fomka_web.Models
{
    public class MainVM
    {
        public List<Task> Tasks;
        public AppUserType Role;
        public List<int> SelectedItems;
    }
}