using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication1.Controllers;
using System.Web.Mvc;

namespace WebApplication1.Test
{
    [TestClass]
    public class HomeControllerUT
    {
        [TestMethod]
        public void About()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.About() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }
    }
}
