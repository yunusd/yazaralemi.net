using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Yazaralemi.Attributes;
using Yazaralemi.Models;

namespace Yazaralemi.Helpers
{
    public static class HtmlHelpers
    {
        public static string ControllerName(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
        }

        public static string ActionName(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewContext.RouteData.Values["action"].ToString();
        }

        public static string PrettyControllerName(this HtmlHelper htmlHelper)
        {
            string controller = htmlHelper.ControllerName();
            Type t = Type.GetType($"Yazaralemi.Areas.Admin.Controllers.{controller}Controller");
            object[] attributes = t.GetCustomAttributes(typeof(BreadcrumbAttribute), true);

            if (attributes.Length == 0)
                return controller;

            var attr = (BreadcrumbAttribute)attributes[0];

            return attr.Name;
        }

        public static string PrettyActionName(this HtmlHelper htmlHelper)
        {
            string controller = htmlHelper.ControllerName();
            string action = htmlHelper.ActionName();
            Type t = Type.GetType($"Yazaralemi.Areas.Admin.Controllers.{controller}Controller");
            MethodInfo mi = t.GetMethods().FirstOrDefault(x => x.Name == action);

            BreadcrumbAttribute ba = mi.GetCustomAttribute(typeof(BreadcrumbAttribute)) as BreadcrumbAttribute;

            if (ba == null)
                return action;

            return ba.Name;
        }

        public static IHtmlString ShowPost(this HtmlHelper htmlHelper, string content, bool full = false)
        {
            var element = "<hr>";
            var pos = content.IndexOf(element);
            if (pos == -1)
                return htmlHelper.Raw(content);

            if (full)
                return htmlHelper.Raw(content.Remove(pos, element.Length));

            return htmlHelper.Raw(content.Substring(0, pos));
        }

        public static string ProfilePhotoPath(this HtmlHelper htmlHelper)
        {
            string userId = htmlHelper.ViewContext.HttpContext.User.Identity.GetUserId();
            UrlHelper urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext, htmlHelper.RouteCollection);

            if (userId != null)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var user = ctx.Users.Find(userId);
                    if (user != null && !string.IsNullOrEmpty(user.Photo))
                    {
                        return urlHelper.Content($"~/Upload/Profiles/{user.Photo}");
                    }
                }
            }
            return urlHelper.Content("~/Images/avatar.png");
        }
    }
}