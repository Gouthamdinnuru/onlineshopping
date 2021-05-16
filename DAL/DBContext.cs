using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CaseStudy.Models;

namespace CaseStudy.DAL
{
    public class DBContext:DbContext
    {

        public DBContext() : base("Case")
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<CustomerOrder> CustomerOrders { get; set; }

        public DbSet<OrderedProduct> Orderedproducts { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<tbl_user> tbl_Users { get; set; }
        public DbSet<Role_tbl> Role_Tbls { get; set; }
        public DbSet<User_tbl> User_Tbls { get; set; }


    }
}
 