using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Yazaralemi.Attributes;
using Yazaralemi.Models;

namespace Yazaralemi.Areas.Admin.Controllers
{
    [Breadcrumb("Kategoriler")]
    public class CategoriesController : AdminBaseController
    {
        // GET: Admin/Categories
        [Breadcrumb("İndeks")]
        public ActionResult Index()
        {
            return View(ctx.Categories.ToList());
        }

        [Breadcrumb("Yeni")]
        public ActionResult New()
        {
            return View(new Category());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult New(Category category)
        {
            if (ModelState.IsValid)
            {
                ctx.Categories.Add(category);
                ctx.SaveChanges();
                TempData["successMessage"] = "Kategori başarıyla eklendi";
                return RedirectToAction("Index");
            }

            return View();
        }

        [Breadcrumb("Düzenle")]
        public ActionResult Edit(int id)
        {
            return View(ctx.Categories.Find(id));
        }


        [HttpPost, ValidateAntiForgeryToken]
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
        public ActionResult Delete(int id)
        {
            var category = ctx.Categories.Find(id);

            if (category == null)
                return HttpNotFound();

            if (category.Posts.Count > 0)
                return Json(new { success = false, error = "Silmek istediğiniz kategoride yazılar bulunduğundan silinememektedir." });

            ctx.Categories.Remove(category);
            ctx.SaveChanges();

            return Json(new { success = true });
        }
    }
}