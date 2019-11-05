using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yazaralemi.Models;

namespace Yazaralemi.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(int? cid, int page = 1)
        {
            int postPerPage = 5;

            ViewBag.SubTitle = "Bir fakirin düşünceleri";
            ViewBag.NextPage = page + 1;
            ViewBag.PrevPage = page - 1;
            ViewBag.cid = cid;
            IQueryable<Post> result = ctx.Posts;
            var cat = ctx.Categories.Find(cid);

            if (cat != null)
            {
                ViewBag.Subtitle = cat.CategoryName;
            }

            if (cid != null)
                result = result.Where(x => x.CategoryId == cid);

            if(result.Count() <= 0)
            {
                return PartialView("_NotFound");
            }

            return View(result.OrderByDescending(x => x.CreatedAt).Skip((page - 1) * postPerPage).Take(postPerPage).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CategoriesPartial()
        {
            return PartialView("_CategoriesPartial", ctx.Categories.ToList());
        }
    }
}