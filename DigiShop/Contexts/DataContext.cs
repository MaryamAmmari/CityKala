using DigiShop.Models.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DigiShop.Models.DataModels.Configuration;
using Microsoft.AspNetCore.Identity;

namespace DigiShop.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DataContext (DbContextOptions<DataContext> options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Product>(new ProductConfiguration());
            modelBuilder.ApplyConfiguration<Feature>(new FeatureConfiguration());
            modelBuilder.ApplyConfiguration<Category>(new CategoryConfiguration());

        }
    }
}
