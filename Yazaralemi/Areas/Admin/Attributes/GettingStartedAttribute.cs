using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Yazaralemi.Models;

namespace Yazaralemi.Areas.Admin.Attributes
{
    public class GettingStartedAttribute : ActionFilterAttribute
    {
        protected ApplicationDbContext ctx = new ApplicationDbContext();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var info = ctx.SiteInfos.SingleOrDefault();
            var controllerName = filterContext.RouteData.Values["controller"].ToString();

            if (info == null && controllerName != "GettingStarted")
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "GettingStarted", action = "Index" })
                );
            }
        }
    }
}