using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yazaralemi.Areas.Admin.ViewModel;
using Yazaralemi.Attributes;
using Yazaralemi.Models;

namespace Yazaralemi.Areas.Admin.Controllers
{
    [Breadcrumb("Gönderiler")]
    public class PostsController : AdminBaseController
    {
        // GET: Admin/Posts
        [HttpGet]
        [Breadcrumb("İndeks")]
        public ActionResult Index()
        {
            return View(ctx.Posts.ToList());
        }

        [HttpGet]
        [Breadcrumb("Yeni")]
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
        [Breadcrumb("Düzenle")]
        public ActionResult Edit(int id)
        {
            ViewBag.categoryId = new SelectList(ctx.Categories.ToList(), "Id", "CategoryName");
            var postVm = ctx.Posts.Select(x => new PostEditViewModel
            {
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

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var post = ctx.Posts.Find(id);
            if (post == null) return Json("failed");
            ctx.Posts.Remove(post);
            ctx.SaveChanges();

            return Json("success");
        }

        [HttpPost]
        public ActionResult AjaxImageUpload(HttpPostedFileBase file)
        {                                                                 
            if (file == null || file.ContentLength == 0 || !file.ContentType.StartsWith("image/"))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var saveFolderPath = Server.MapPath("~/Upload/Posts");
            var ext = Path.GetExtension(file.FileName);
            var saveFileName = Guid.NewGuid() + ext;
            var saveFilePath = Path.Combine(saveFolderPath, saveFileName);
            file.SaveAs(saveFilePath);

            return Json(new { url = Url.Content($"~/Upload/Posts/{saveFileName}")});
        }
    }
}