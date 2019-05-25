using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BackEnd.Models
{
    public class ShopDbContext: IdentityDbContext<User>
    {
        public ShopDbContext(): base("StoreDB")
        {
            //Database.SetInitializer<ShopDbContext>(new DropCreateDatabaseIfModelChanges<ShopDbContext>());
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}