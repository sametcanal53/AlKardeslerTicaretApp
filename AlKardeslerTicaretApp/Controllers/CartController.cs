using AlKardeslerTicaretApp.Helper;
using AlKardeslerTicaretApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlKardeslerTicaretApp.Controllers
{
    public class CartController : Controller
    {
        private hDB db = new hDB();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }

        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.FirstOrDefault(item => item.Id == Id);
            if(product != null)
            {
                GetCart().AddProduct(product,1);
            }
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.FirstOrDefault(item => item.Id == Id);
            
            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }
            return RedirectToAction("Index");
        }

        public mCart GetCart()
        {
            var cart = (mCart)Session["Cart"];
            if(cart == null)
            {
                cart = new mCart();
                Session["Cart"] = cart;
            }
            return cart;
        }


        public ActionResult Shipping()
        {
            if(Session["User"]!= null)
            {
                return View(new mShipping());
            }
            else
            {
                return RedirectToAction("Login","Account");

            }
        }

        [HttpPost]
        public ActionResult Shipping(mShipping shipping)
        {
            var cart = GetCart();

            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("", "Sepette Ürün Bulunmamaktadır");
            }
            if(ModelState.IsValid)
            {
                SaveOrder(cart, shipping);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View();
            }
        }

        private void SaveOrder(mCart cart, mShipping shipping)
        {
            var order = new mOrder();
            order.OrderNumber = (DateTime.Now.Year + "" + DateTime.Now.Month + "" + DateTime.Now.Day + (new Random()).Next(11111111,99999999)).ToString();
            order.Total = cart.Total();
            order.OrderState = eEnumOrderState.Waiting;
            order.OrderDate = DateTime.Now;
            order.Username = shipping.Username;
            order.Baslik = shipping.Baslik;
            order.Adres = shipping.Adres;
            order.Sehir = shipping.Sehir;
            order.Ilce = shipping.Ilce;
            order.Orderlines = new List<OrderLine>();

            foreach(var item in cart.CartLines)
            {
                var orderline = new OrderLine();
                orderline.Quantity = item.Quantity;
                orderline.Price = item.Quantity * item.Product.Price;
                orderline.ProductId = item.Product.Id;
                order.Orderlines.Add(orderline);
            }
            db.Orders.Add(order);
            db.SaveChanges();
        }

    }
}