using E_commerce.Presistence.Data.DBContexts;
using E_Commerce.Domain.Contract.DataIdentifier;
using E_Commerce.Domain.Entity;
using E_Commerce.Domain.Entity.ProductEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_commerce.Presistence.Datase
{
    public class Dataseed : Idataseeding
    {
        private readonly StoreDbContext context;

        public Dataseed(StoreDbContext context)
        {
            this.context = context;
        }


        public   async Task insilizeAsync()
        {
            try
            {
                 var HasBrands = await context.productBrands.AnyAsync();
                var HasProductType = await context.productTypes.AnyAsync();
                var HasProduct= await context.products.AnyAsync();
                if (HasBrands && HasProductType  &&  HasProduct) return;

                if(! HasBrands)
                {
                  await  seeddatafromJason<ProductBrand, int>("brands.json", context.productBrands);
                }
                if (!HasProductType)
                {
                   await seeddatafromJason<ProductType, int>("types.json", context.productTypes);


                }
                 await context.SaveChangesAsync();

                if (! HasProduct)
                {
                   await seeddatafromJason<Product, int>("products.json", context.products);

                }
                await context.SaveChangesAsync();



            }
            catch(Exception ex) {
                Console.WriteLine($"Data seeding failed {ex}");
            
            
            
            
            }

        }



       private async Task seeddatafromJason<T, Tkey>( string filename,DbSet<T> entities) where T:BaseEntity<Tkey>
        {
            try
            {
                string filepath = @"..\E_commerce.Presistence\JsonFiles\" + filename;

                if (!File.Exists(filepath)) return;
                using var DataStream = File.OpenRead(filepath);

                var data = await JsonSerializer.DeserializeAsync<List<T>>(DataStream, new JsonSerializerOptions());

                if (data is not null)
                {
                    await  entities.AddRangeAsync(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error While Reading JSON File {ex}");
            }






        }
    }
}
