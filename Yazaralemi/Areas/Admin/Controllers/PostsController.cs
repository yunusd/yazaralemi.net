using Microsoft.AspNet.Identity;
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

        [HttpGet]
        public ActionResult New()
        {
            ViewBag.categoryId = new SelectList(ctx.Categories.ToList(), "Id", "CategoryName");

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult New(PostNewViewModel post)
        {
            if (ModelState.IsValid)
            {
                var newPost = new Post
                {
                    AuthorId = ctx.Users.Find(User.Identity.GetUserId()).Id,
                    Title = post.Title,
                    Content = post.Content,
                    ContentSummary = post.ContentSummary,
                    CategoryId = post.CategoryId,
                    CreatedAt = DateTime.Now
                };
                ctx.Posts.Add(newPost);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryId = new SelectList(ctx.Categories.ToList(), "Id", "CategoryName");

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.categoryId = new SelectList(ctx.Categories.ToList(), "Id", "CategoryName");
            var postVm = ctx.Posts.Select(x => new PostEditViewModel {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
                ContentSummary = x.ContentSummary,
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
                post.ContentSummary = model.ContentSummary;
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryId = new SelectList(ctx.Categories.ToList(), "Id", "CategoryName");
            return View();
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
    }
}