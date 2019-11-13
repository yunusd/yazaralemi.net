using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yazaralemi.Models;
using Yazaralemi.ViewModels;

namespace Yazaralemi.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index(int? cid, int page = 1)
        {
            int postPerPage = 5;


            IQueryable<Post> result = ctx.Posts;

            ViewBag.subTitle = "Bir fakirin düşünceleri";
            ViewBag.nextPage = page + 1;
            ViewBag.prevPage = page - 1 < 2 ? 1 : page - 1;
            ViewBag.cid = cid;
            ViewBag.page = page;
            var cat = ctx.Categories.Find(cid);

            if (cat != null)
                ViewBag.Subtitle = cat.CategoryName;

            if (cid != null)
                result = result.Where(x => x.CategoryId == cid);

            if (result.Count() <= 0)
                return PartialView("_NotFound");

            ViewBag.pageCount = Math.Ceiling(result.Count() / (decimal)postPerPage);
            return View(result.OrderByDescending(x => x.CreatedAt).Skip((page - 1) * postPerPage).Take(postPerPage).ToList());
        }

        public ActionResult ShowPost(int id)
        {
            var post = ctx.Posts.Find(id);
            if (post == null)
                return HttpNotFound();
            return View(post);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendComment(SendCommentViewModel comment)
        {

            if (ModelState.IsValid)
            {
                Comment newComment = new Comment
                {
                    AuthorId = User.Identity.GetUserId(),
                    AuthorName = comment.AuthorName,
                    AuthorEmail = comment.AuthorEmail,
                    Content = comment.Content,
                    CreatedAt = DateTime.Now,
                    ParentId = comment.ParentId,
                    PostId = comment.PostId
                };

                ctx.Comments.Add(newComment);
                ctx.SaveChanges();
                return Json(newComment);
            }
            var errorList = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();

            return Json(new { Errors = errorList });
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