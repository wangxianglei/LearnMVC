﻿using System.Web;
using System.Web.Mvc;

namespace MicrosoftAzure.Tutorial.SendGrid
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}