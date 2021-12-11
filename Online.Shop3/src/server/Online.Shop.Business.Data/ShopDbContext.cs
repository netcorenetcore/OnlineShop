using Microsoft.EntityFrameworkCore;
using Online.Shop.Business.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Online.Shop.Business.Data
{
    public class ShopDbContext:DbContext
    {
        public ShopDbContext(DbContextOptions options):base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Store>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Sale>().HasQueryFilter(x => !x.IsDeleted);
            base.OnModelCreating(modelBuilder);
        }
    }
}
