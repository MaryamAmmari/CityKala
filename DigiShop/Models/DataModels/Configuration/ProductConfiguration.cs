using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigiShop.Models.DataModels.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
 
            builder.HasKey(i => i.ProductId);
            builder.Property(i => i.ProductId).ValueGeneratedNever();
            builder.ToTable("Products");

        }
    }
}
