using AlKardeslerTicaretApp.Helper;
using AlKardeslerTicaretApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace AlKardeslerTicaretApp.Controllers
{
    public class AccountController : Controller
    {
        hDB db = new hDB();
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(mUser user)
        {
            user.Role = "User";
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("RegisterCompleted","Account");
        }
        public ActionResult RegisterCompleted()
        {
            return View();
        }

        public ActionResult Login()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
            
        }

        [HttpPost]
        public ActionResult Login(mUser user)
        {
                var result = db.Users.FirstOrDefault(i => i.Email == user.Email && i.Password == user.Password);
                if (result != null)
                {
                    Session["User"] = result;
                    return RedirectToAction("Completed", "Account");
                }
                else
                {
                    Session["User"] = null;
                    return RedirectToAction("Error", "Account");
                }
            
            
        }

        public ActionResult LogOut()
        {
            Session["User"] = null;
            return View();
        }
        public ActionResult Completed()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

       
    }
}