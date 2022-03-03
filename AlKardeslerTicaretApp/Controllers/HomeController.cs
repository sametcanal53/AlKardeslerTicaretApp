using AlKardeslerTicaretApp.Helper;
using AlKardeslerTicaretApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlKardeslerTicaretApp.Controllers
{
    public class HomeController : Controller
    {
        hDB db = new hDB();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string s)
        {
            var find = from item in db.Products select item;
            if (!String.IsNullOrEmpty(s))
            {
                find = find.Where(item => item.Description.Contains(s) || item.Name.Contains(s));
            }
            return View(find.ToList());
        }

        public ActionResult List(int? id)
        {
            var product = db.Products.AsQueryable();
            if (id != null)
            {
                product = product.Where(item => item.CategoryId == id);
            }
            return View(product.ToList());
        }

        public ActionResult Details(int id)
        {
            return View(db.Products.FirstOrDefault(i => i.Id == id));
        }

    }
}