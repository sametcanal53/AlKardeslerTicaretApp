using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlKardeslerTicaretApp.Models
{
    public class mOrder
    {
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public eEnumOrderState OrderState { get; set; }
        public  List<OrderLine> Orderlines { get; set; }
        public string Username { get; set; }
        public string Baslik { get; set; }
        public string Adres { get; set; }
        public string Ilce { get; set; }
        public string Sehir { get; set; }
    }

    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public mOrder Order { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public mProduct Product { get; set; }


    }
}