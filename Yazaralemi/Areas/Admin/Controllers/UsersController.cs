using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yazaralemi.Attributes;

namespace Yazaralemi.Areas.Admin.Controllers
{
    [Breadcrumb("Kullanıcılar")]
    public class UsersController : AdminBaseController
    {
        // GET: Admin/Users
        [Breadcrumb("İndeks")]
        public ActionResult Index()
        {
            return View(ctx.Users.ToList());
        }

        [HttpPost]
        public ActionResult ChangeStatus(string userId, bool isEnabled)
        {
            if (userId != null)
            {
                var user = ctx.Users.Find(userId);

                if (user != null && user.Id != User.Identity.GetUserId())
                {
                    user.IsEnabled = isEnabled;
                    ctx.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { error = "kullanıcı bulunamadı" });
            }

            return View();
        }
    }
}