
using E_commerce.Presistence.Data.DBContexts;
using E_commerce.Presistence.Datase;
using E_commerce.Presistence.Repository;
using E_commerce.Services;
using E_commerce.Services.MappingProfile;
using E_commerce.Services_Abstraction;
using E_commerce.web.Extenstions;
using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Contract.DataIdentifier;
using E_Commerce.Domain.Contract.GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace E_commerce.web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

          
            # region Add services to the container
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<Idataseeding, Dataseed>();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(x => x.AddProfile<ProductProfile>());
            builder.Services.AddAutoMapper(x => x.AddProfile<BasketProfile>());

            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddSingleton<ProductPictureUrlResolver>();
            builder.Services.AddSingleton<IConnectionMultiplexer>(CM =>
                {
                    return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!);
                }

            );
            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<IBasketServcies, BasketServices>();

            builder.Services.AddScoped<IRedisRepo, RedisRepo>();

            builder.Services.AddScoped<IRedisServices, RedisServices>();

            #endregion


            var app = builder.Build();
            #region Dataseed
            await app.migrateDataSeeding();
            await app.DataSeeding();
            #endregion
            
            #region Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            


            app.MapControllers();
            #endregion

            await app.RunAsync();
        }
    }
}
