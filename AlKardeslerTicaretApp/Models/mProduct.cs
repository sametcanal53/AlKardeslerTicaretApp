using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlKardeslerTicaretApp.Models
{
    public class mProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public int CategoryId { get; set; }
        public mCategory Category { get; set; }
        public string Image { get; set; }
    }
}