using ProASPNETMVC5.Filters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProASPNETMVC5.Filters.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Users ="admin")]
        public string Index()
        {
            return "This is Index action on the Home controller";
        }

        [GoogleAuth]
        [Authorize(Users = "wangxianglei@google.com")]
        public string List()
        {
            return "This is List action on the Home controller";
        }
    }
}