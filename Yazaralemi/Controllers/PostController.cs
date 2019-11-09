using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Yazaralemi.Controllers
{
    public class PostController : BaseController
    {
        [HttpGet]
        public ActionResult Index(int cid, int pid)
        {
            var category = ctx.Categories.Find(cid);
            var post = ctx.Posts.Find(pid);
            if(category == null || post == null) return PartialView("_NotFound");

            return View(post);
        }
    }
}