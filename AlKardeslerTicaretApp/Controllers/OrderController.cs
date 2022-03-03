using AlKardeslerTicaretApp.Helper;
using AlKardeslerTicaretApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlKardeslerTicaretApp.Controllers
{
    public class OrderController : Controller
    {
        hDB db = new hDB();
        // GET: Order
        public ActionResult Index()
        {
            if (Session["User"] != null)
            {
                var username = (Session["User"] as mUser).Username;
                var orders = db.Orders.Where(item => item.Username == username).Select(item=> new mUserOrder()
                {
                    Id = item.Id,
                    OrderNumber = item.OrderNumber,
                    OrderDate = item.OrderDate,
                    OrderState = item.OrderState,
                    Total = item.Total
                }).OrderByDescending(item => item.OrderDate).ToList();
                return View(orders);
            }
            else
            {
                return RedirectToAction("Index");

            }
        }

        public ActionResult OrderDetail(int id)
        {
            if (Session["User"] != null)
            {
                var orders = db.Orders.Where(item => item.Id == id).Select(item => new mOrderDetail()
                {

                    OrderId = item.Id,
                    OrderNumber = item.OrderNumber,
                    Total = item.Total,
                    OrderDate = item.OrderDate,
                    OrderState = item.OrderState,
                    Baslik = item.Baslik,
                    Adres = item.Adres,
                    Sehir = item.Sehir,
                    Ilce = item.Ilce,
                    Username = item.Username,
                    Orderlines = item.Orderlines.Select(i => new mOrderLineDetail()
                    {
                        ProductId = i.ProductId,
                        ProductName = i.Product.Name+" "+i.Product.Description,
                        Price = i.Price,
                        Image = i.Product.Image,
                        Quantity = i.Quantity
                    }).ToList()
                    
                }).FirstOrDefault();
                return View(orders);
            }
            else
            {
                return RedirectToAction("Index");

            }


        }


        public ActionResult UpdateOrderState(int OrderId,eEnumOrderState orderState)
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                var order = db.Orders.FirstOrDefault(item => item.Id == OrderId);

                if(order != null)
                {
                    order.OrderState = orderState;
                    db.SaveChanges();
                    return RedirectToAction("AdminDetail",new { id = OrderId });
                }
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");

            }


        }


        public ActionResult AdminIndex()
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                var orders = db.Orders.Select(item => new mAdminOrder()
                {
                    Id = item.Id,
                    OrderNumber = item.OrderNumber,
                    OrderDate = item.OrderDate,
                    OrderState = item.OrderState,
                    Total = item.Total
                }).ToList();
                return View(orders);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }

        public ActionResult AdminDetail(int id)
        {
            if (Session["User"] != null && (Session["User"] as mUser).Role == "Admin")
            {
                var orders = db.Orders.Where(item => item.Id == id).Select(item => new mOrderDetail()
                {

                    OrderId = item.Id,
                    OrderNumber = item.OrderNumber,
                    Total = item.Total,
                    OrderDate = item.OrderDate,
                    OrderState = item.OrderState,
                    Baslik = item.Baslik,
                    Adres = item.Adres,
                    Sehir = item.Sehir,
                    Ilce = item.Ilce,
                    Username = item.Username,
                    Orderlines = item.Orderlines.Select(i => new mOrderLineDetail()
                    {
                        ProductId = i.ProductId,
                        ProductName = i.Product.Name + " " + i.Product.Description,
                        Price = i.Price,
                        Image = i.Product.Image,
                        Quantity = i.Quantity
                    }).ToList()

                }).FirstOrDefault();
                return View(orders);
            }
            else
            {
                return RedirectToAction("Index", "Home");

            }
        }


    }
}