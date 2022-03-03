using AlKardeslerTicaretApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AlKardeslerTicaretApp.Helper
{
    public class hDB:DbContext
    {
        public hDB() : base("Server=.;Database=AlKardeslerTicaretDB;Trusted_Connection=True;")
        {

        }
        public DbSet<mProduct> Products { get; set; }
        public DbSet<mCategory> Categories { get; set; }
        public DbSet<mOrder> Orders { get; set; }
        public DbSet<mUser> Users { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }
        
    }
}