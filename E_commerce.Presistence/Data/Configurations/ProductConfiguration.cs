using E_Commerce.Domain.Entity.ProductEntity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Presistence.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x=>x.PictureUrl).HasMaxLength(100);
            builder.Property(x => x.Price).HasPrecision(18, 2);
            builder.HasOne(x => x.productBrand).WithMany().HasForeignKey(x => x.BrandId);
            builder.HasOne(x => x.productType).WithMany().HasForeignKey(x => x.TypeId);

        }
    }
}
