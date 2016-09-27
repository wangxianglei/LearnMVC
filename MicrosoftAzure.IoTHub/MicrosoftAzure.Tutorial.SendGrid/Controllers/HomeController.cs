using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using SendGrid;
using System.IO;

namespace MicrosoftAzure.Tutorial.SendGrid.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var message = new SendGridMessage();
            message.From = new MailAddress("xiangleiwang@outlook.com");
            List<string> recipients = new List<string> {
                @"xianglei@ncs.com.sg",
                @"brucewonger@gmail.com"
            };
            message.AddTo(recipients);
            message.Subject = "Microsoft Azure: SendGrid Library";
            message.Html = "<p>Hello World!</p>";
            message.Text = "Hello World Text";

            //message.AddAttachment("C:\\1.txt");
            //using (var fileStream = new FileStream("C:\1.txt", FileMode.Open))
            //{
            //    message.AddAttachment(fileStream, "Email Attachment.txt");
            //}

            message.EnableFooter("PLAIN TEXT FOOTER", "<p><em>Html footer</em></p>");
            message.EnableClickTracking(true);

            var credentials = new NetworkCredential("azure_cc21af0525d2042119c44a8df95d2196@azure.com", "Password123");
            var webTransport = new Web(credentials);
            webTransport.DeliverAsync(message);

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}