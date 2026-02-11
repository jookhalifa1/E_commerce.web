using E_Commerce.Domain.Entity.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Presistence.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.SubTotal).HasPrecision(8, 2);
           

            builder.OwnsOne(x => x.Address, x =>
            {
                x.Property(m => m.Street).HasMaxLength(50);
                x.Property(m => m.City).HasMaxLength(50);
                x.Property(m => m.Country).HasMaxLength(50);
                x.Property(m => m.FirstName).HasMaxLength(50);
                x.Property(m => m.LastName).HasMaxLength(50);
            });

            
        }
    }
}
