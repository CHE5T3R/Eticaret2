using Eticaret.Entities;
using Eticaret.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Collections.Generic;

namespace Eticaret.Entities
{
    public class DataContext : DbContext
    {
        public DataContext() : base("dataConnection")
        {
            Database.SetInitializer(new DataInitializer());
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


    }
}