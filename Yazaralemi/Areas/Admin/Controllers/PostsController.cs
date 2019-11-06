using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yazaralemi.Areas.Admin.ViewModel;
using Yazaralemi.Models;

namespace Yazaralemi.Areas.Admin.Controllers
{
    public class PostsController : AdminBaseController
    {
        // GET: Admin/Posts
        [HttpGet]
        public ActionResult Index()
        {
            return View(ctx.Posts.ToList());
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var post = ctx.Posts.Find(id);
            if (post == null) return Json("failed");
            ctx.Posts.Remove(post);
            ctx.SaveChanges();

            return Json("success");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.categoryId = new SelectList(ctx.Categories.ToList(), "Id", "CategoryName");
            var postVm = ctx.Posts.Select(x => new PostEditViewModel {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                CategoryId = x.CategoryId,
            }).FirstOrDefault(x => x.Id == id);
            if (postVm == null) return HttpNotFound();

            return View(postVm);
        }
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PostEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Post post = ctx.Posts.Find(model.Id);

                post.Title = model.Title;
                post.Content = model.Content;
                post.CategoryId = model.CategoryId;
                ctx.SaveChanges();
                //ctx.Entry(model).State = EntityState.Modified;
                //ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryId = new SelectList(ctx.Categories.ToList(), "Id", "CategoryName");
            return View();
        }
    }
}