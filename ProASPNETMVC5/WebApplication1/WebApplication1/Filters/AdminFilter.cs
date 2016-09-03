﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Filters
{
    public class AdminFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Convert.ToBoolean(filterContext.HttpContext.Session["Admin"]))
            {
                filterContext.Result = new ContentResult
                {
                    Content = "Unauthorized to access specified resource"
                };
            }
        }
    }
}