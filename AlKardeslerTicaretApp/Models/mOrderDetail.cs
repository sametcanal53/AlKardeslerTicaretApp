using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlKardeslerTicaretApp.Models
{
    public class mOrderDetail
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public double Total { get; set; }
        public DateTime OrderDate { get; set; }
        public eEnumOrderState OrderState { get; set; }
        public  List<mOrderLineDetail> Orderlines { get; set; }
        public string Username { get; set; }
        public string Baslik { get; set; }
        public string Adres { get; set; }
        public string Ilce { get; set; }
        public string Sehir { get; set; }
    }

    public class mOrderLineDetail
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}