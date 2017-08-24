using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Configuration;

namespace MvcShopping.Models
{
    public class MvcShoppingContext:DbContext
    {
        static string con = ConfigurationManager.ConnectionStrings["MvcShopping"].ToString();

        public MvcShoppingContext() : base(con)
        {

        }
        public DbSet<Member> Members { get; set; }
        public DbSet<OrderDetail> OrderDetailsItems { get; set; }
        public DbSet<OrderHeader> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategorys { get; set; }
    }
}