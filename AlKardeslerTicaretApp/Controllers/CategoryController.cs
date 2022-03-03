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
    
    public class CategoryController : Controller
    {
        private hDB db = new hDB();

        // GET: Category
        public ActionResult Index()
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                return View(db.Categories.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

        // GET: Category/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                mCategory mCategory = db.Categories.Find(id);
                if (mCategory == null)
                {
                    return HttpNotFound();
                }
                return View(mCategory);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
            
        
        }

        // GET: Category/Create
        public ActionResult Create()
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

        // POST: Category/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] mCategory mCategory)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(mCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mCategory);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                mCategory mCategory = db.Categories.Find(id);
                if (mCategory == null)
                {
                    return HttpNotFound();
                }
                return View(mCategory);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

        // POST: Category/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] mCategory mCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mCategory);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                mCategory mCategory = db.Categories.Find(id);
                if (mCategory == null)
                {
                    return HttpNotFound();
                }
                return View(mCategory);
            }
           


            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mCategory mCategory = db.Categories.Find(id);
            db.Categories.Remove(mCategory);
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
