using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Yazaralemi.Models;

namespace Yazaralemi.Areas.Admin.Controllers
{
    public class CategoriesController : AdminBaseController
    {

        // GET: Admin/Categories
        [HttpGet]
        public ActionResult Index()
        {
            return View(ctx.Categories.ToList());
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult New(Category category)
        {
            if (ModelState.IsValid)
            {
                ctx.Categories.Add(category);
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int cid)
        {
            var category = ctx.Categories.Find(cid);

            if (category == null) return HttpNotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                ctx.Entry(category).State = EntityState.Modified;
                ctx.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int cid, bool deletePosts)
        {
            string uncategorized = "[Kategorilendirilmemiş]";
            var category = ctx.Categories.Find(cid);

            if (category == null) return Json("failed");

            var posts = ctx.Posts.Where(x => x.CategoryId == category.Id);


            if (!deletePosts)
            {
                // if category name is uncategorized then return cascade_delete response
                // because posts can't move to a category they already in.
                if (category.CategoryName == uncategorized) return Json("cascade_delete");
                await MovePosts(posts, uncategorized);
            }
            else
            {
                ctx.Posts.RemoveRange(posts);
            }

            ctx.Categories.Remove(category);
            ctx.SaveChanges();

            return Json("success");
        }

        private async Task MovePosts(IQueryable<Post> posts, string categoryName)
        {
            var category = ctx.Categories.SingleOrDefault(x => x.CategoryName == categoryName);
            if (category == null)
            {
                var newCategory = ctx.Categories.Add(new Category { CategoryName = categoryName });
                ctx.SaveChanges();
                category = newCategory;
            }

            await posts.ForEachAsync((post) => { post.CategoryId = category.Id; }); // change category of all posts that met the condition
        }
    }
}