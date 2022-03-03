using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlKardeslerTicaretApp.Helper;
using AlKardeslerTicaretApp.Models;

namespace AlKardeslerTicaretApp.Controllers
{
    public class ProductController : Controller
    {
        private hDB db = new hDB();

        // GET: Product
        public ActionResult Index()
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                var products = db.Products.Include(m => m.Category);
                return View(products.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

        // POST: Product/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,CategoryId,Image")] mProduct mProduct)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(mProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", mProduct.CategoryId);
            return View(mProduct);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                mProduct mProduct = db.Products.Find(id);
                if (mProduct == null)
                {
                    return HttpNotFound();
                }
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", mProduct.CategoryId);
                return View(mProduct);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }


        // POST: Product/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,CategoryId,Image")] mProduct mProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", mProduct.CategoryId);
            return View(mProduct);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                mProduct mProduct = db.Products.Find(id);
                if (mProduct == null)
                {
                    return HttpNotFound();
                }
                return View(mProduct);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mProduct mProduct = db.Products.Find(id);
            db.Products.Remove(mProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
