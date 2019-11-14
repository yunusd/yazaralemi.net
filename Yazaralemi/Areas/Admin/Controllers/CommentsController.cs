using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yazaralemi.Attributes;

namespace Yazaralemi.Areas.Admin.Controllers
{
    [Breadcrumb("Yorumlar")]
    public class CommentsController : AdminBaseController
    {
        // GET: Admin/Comments
        [Breadcrumb("İndeks")]
        public ActionResult Index()
        {
            return View(ctx.Comments.ToList());
        }
    }
}