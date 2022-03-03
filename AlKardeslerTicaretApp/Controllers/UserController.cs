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
    public class UserController : Controller
    {
        private hDB db = new hDB();

        // GET: User
        public ActionResult Index()
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                return View(db.Users.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }

        }


        // GET: User/Create
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

        // POST: User/Create
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Firstname,Lastname,Username,Email,Password,Role")] mUser mUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(mUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mUser);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                mUser mUser = db.Users.Find(id);
                if (mUser == null)
                {
                    return HttpNotFound();
                }
                return View(mUser);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }

        }

        // POST: User/Edit/5
        // Aşırı gönderim saldırılarından korunmak için, bağlamak istediğiniz belirli özellikleri etkinleştirin, 
        // daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Firstname,Lastname,Username,Email,Password,Role")] mUser mUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mUser);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                mUser mUser = db.Users.Find(id);
                if (mUser == null)
                {
                    return HttpNotFound();
                }
                return View(mUser);
            }

            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            mUser mUser = db.Users.Find(id);
            db.Users.Remove(mUser);
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
