using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProASPNETMVC5.Filters.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string returnUrl)
        {
            bool isAuth = FormsAuthentication.Authenticate(username, password);

            if (isAuth)
            {
                FormsAuthentication.SetAuthCookie(username, false);

                return Redirect(returnUrl ?? Url.Action("Index", "Home"));
            }
            else
            {
                ModelState.AddModelError("", "Incorrect username or password");

                return View();
            }
        }
    }
}