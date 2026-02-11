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
    public class orderItemConfiguration : IEntityTypeConfiguration<ItemsOfOrder>
    {
        public void Configure(EntityTypeBuilder<ItemsOfOrder> builder)
        {
            builder.Property(x => x.Price).HasPrecision(8, 2);
            builder.OwnsOne(x => x.productItemOrder, c =>
            {
                c.Property(x => x.Name).HasMaxLength(100);
                c.Property(x => x.PictureUrl).HasMaxLength(200);
            });
        }
    }
}
