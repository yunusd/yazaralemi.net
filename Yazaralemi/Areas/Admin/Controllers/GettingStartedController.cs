using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using Yazaralemi.Areas.Admin.ViewModel;
using Yazaralemi.Models;

namespace Yazaralemi.Areas.Admin.Controllers
{
    public class GettingStartedController : AdminBaseController
    {
        // GET: Admin/GettingStarted
        public ActionResult Index()
        {
            if (ctx.SiteInfos.Count() == 0)
            {
                return PartialView("_GettingStarted");
            }
            return RedirectToAction("Index", "Dashboard");
        }

        // GET: Admin/GettingStarted
        [HttpPost]
        public ActionResult Index(GettingStartedViewModel vm)
        {
            if (ModelState.IsValid && ctx.SiteInfos.Count() == 0)
            {
                var user = ctx.Users.Find(User.Identity.GetUserId());

                var siteInfo = new SiteInfo
                {
                    SiteName = vm.SiteName,
                    About = vm.About,
                    Contact = vm.Contact,
                    Footer = vm.Footer.Replace("[copy]", "&copy;").Replace("[date]", DateTime.Now.Year.ToString()),
                };

                ctx.SiteInfos.Add(siteInfo);

                user.FirstName = vm.FirstName;
                user.LastName = vm.LastName;

                ctx.Entry(user).State = EntityState.Modified;
                //todo: user changes(firstname,lastname) won't effective to application until logout and login again. need fix this problem.
                ctx.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}