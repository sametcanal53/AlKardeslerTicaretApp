using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlKardeslerTicaretApp.Models
{
    public class mCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<mProduct> Products { get; set; }
    }
}