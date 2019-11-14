using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yazaralemi.Areas.Admin.ViewModel;
using Yazaralemi.Attributes;

namespace Yazaralemi.Areas.Admin.Controllers
{
    [Breadcrumb("Anasayfa")]
    public class DashboardController : AdminBaseController
    {

        // GET: Admin/Dashboard
        [Breadcrumb("İndeks")]
        public ActionResult Index()
        {
            var model = new DashboardIndexViewModel
            {
                CategoryCount = ctx.Categories.Count(),
                PostCount = ctx.Posts.Count(),
                UserCount = ctx.Users.Count(),
                CommentCount = ctx.Comments.Count()
            };
            return View(model);
        }
    }
}