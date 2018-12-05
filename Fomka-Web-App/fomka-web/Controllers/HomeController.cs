using fomka_web.Attributes;
using fomka_web.DAL;
using fomka_web.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using fomka_web.DAL;
using fomka_web.Domain;
using fomka_web.Models;
using System.Web.Script.Serialization;
using System.Web.Security;
using static fomka_web.Attributes.RoleAuthorizeAttribute;

namespace fomka_web.Controllers
{
    [AuthorizeWithRedirect]
    public class HomeController : BaseController
    {
        private MainVM vm = new MainVM();
        private MainRepo dbRepo = new MainRepo();

        public ActionResult Index(int? id = null)
        {
            vm.Role = GeLoginInfo().Type;

            id = id ?? 0;
            if (id != 0)
            {
                vm.OpenedModuleId = id.Value;
                vm.OpenedModule = dbRepo.GetModules().SingleOrDefault(m => m.Id == id);
                var tasks = dbRepo
                    .GeTasks();
                vm.Tasks = dbRepo
                    .GeTasks()
                    .Where(t => t.ModuleId == id)
                    .ToList();

                vm.User = dbRepo.GetUserByUsername(GeLoginInfo().Username);
                vm.ModulesTree = GetDefaultTree(id.Value);

                // use to select in left menu

                return View(vm);
            }
            else
            {
                vm.OpenedModuleId = id.Value;
                vm.User = dbRepo.GetUserByUsername(GeLoginInfo().Username);

                vm.ModulesTree = GetDefaultTree(id.Value);

                // use to select in left menu

                return View(vm);
            }
        }

        private TreeViewItem GetDefaultTree(int selectedId)
        {
            return new TreeViewItem("Програмна інженерія")
            {
                SubItems = new List<TreeViewItem>()
                {
                    new TreeViewItem("Аналіз вимог"),
                    new TreeViewItem("Проектування та моделювання")
                    {
                        SubItems = new List<TreeViewItem>()
                        {
                            //struct and arc
                            new TreeViewItem("Структура і архітектура ПЗ")
                            {
                                SubItems = new List<TreeViewItem>()
                                {
                                    new TreeViewItem("Архітектурні структури"),
                                    new TreeViewItem("Архітектурні стилі"),
                                    new TreeViewItem("Шаблони проектування")
                                    {
                                        SubItems = new List<TreeViewItem>()
                                        {
                                            new TreeViewItem("Твірні шаблони")
                                            {
                                                SubItems = dbRepo
                                                .GetModules()
                                                .Select(m => new TreeViewItem(m.Id, m.Title,(m.Id==selectedId)))
                                            },
                                            new TreeViewItem("Поведінкові шаблони"),
                                            new TreeViewItem("Структурні шаблони"),
                                        }
                                    },
                                    new TreeViewItem("Сімейства програм")
                                }
                            },
                            new TreeViewItem("Аналіз якості програмного дизайну"),
                            new TreeViewItem("Нотації проектування ПЗ"),
                            new TreeViewItem("Стратегії і методи проектування"),
                        },
                    },
                            new TreeViewItem("Кодування"),
                            new TreeViewItem("Тестування"),
                            new TreeViewItem("Супровід")
                }
            };
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            var vm = new LoginInfoWrapper()
            {
                //Username = "Bbrizhaty",
                //Password = "lolkekchebureK1"
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(LoginInfoWrapper objUser)
        {
            if (!ModelState.IsValid)
                return View(objUser);

            var dbUser = Context.Users.FirstOrDefault(u => u.Login == objUser.Username);

            if (dbUser == null || dbUser.Password != objUser.Password)
            {
                ModelState.AddModelError(nameof(LoginInfoWrapper.Username), "Wrong username of password");
                return View(objUser);
            }
            objUser.Type = (AppUserType)dbUser.Type;

            SetLoggedUserInfo(objUser);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [RoleAuthorize(UserType = AppUserType.Lecturer)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction(nameof(Index));
        }
    }
}