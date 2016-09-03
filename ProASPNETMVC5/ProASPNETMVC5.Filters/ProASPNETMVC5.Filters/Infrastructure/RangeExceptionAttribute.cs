using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProASPNETMVC5.Filters.Infrastructure
{
    public class RangeExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled && filterContext.Exception is ArgumentOutOfRangeException)
            {
                //filterContext.Result = new RedirectResult("~/Content/RangeErrorPage.html");
                int actualValue = (int)((ArgumentOutOfRangeException)filterContext.Exception).ActualValue;
                filterContext.Result = new ViewResult { ViewName = "RangeError", ViewData = new ViewDataDictionary<int>(actualValue) };
                filterContext.ExceptionHandled = true;
            }
        }
    }
}