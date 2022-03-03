using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlKardeslerTicaretApp.Models
{
    public class mCart
    {
        private List<mCartLine> _cartLines = new List<mCartLine>();
        public List<mCartLine> CartLines
        {
            get { return _cartLines; }
        }

        public void AddProduct(mProduct product,int quantity)
        {
            var line = _cartLines.FirstOrDefault(item => item.Product.Id == product.Id);
            if(line == null)
            {
                _cartLines.Add(new mCartLine() { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public void DeleteProduct(mProduct product)
        {
            _cartLines.RemoveAll(item => item.Product.Id == product.Id);
        }
        public double Total()
        {
            return _cartLines.Sum(item => item.Product.Price * item.Quantity);
        }
        public void Clear()
        {
            _cartLines.Clear();
        }
    }

    public class mCartLine
    {
        public mProduct Product { get; set; }
        public int Quantity { get; set; }
    }
}