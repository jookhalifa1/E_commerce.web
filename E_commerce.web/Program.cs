
using E_commerce.Presistence.Data.DBContexts;
using E_commerce.Presistence.Data.IdentityDbContexts;
using E_commerce.Presistence.Datase;
using E_commerce.Presistence.Datase.IdintityDataseeding;
using E_commerce.Presistence.Repository;
using E_commerce.Services;
using E_commerce.Services.MappingProfile;
using E_commerce.Services_Abstraction;
using E_commerce.web.CustomMiddleWear;
using E_commerce.web.Extenstions;
using E_Commerce.Domain.Contract;
using E_Commerce.Domain.Contract.DataIdentifier;
using E_Commerce.Domain.Contract.GenericRepository;
using E_Commerce.Domain.Entity.IdentityModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Runtime.CompilerServices;
using System.Text;
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
            builder.Services.AddKeyedScoped<Idataseeding, Dataseed>("Default");
            builder.Services.AddKeyedScoped<Idataseeding,  dentitySeed>("Identity");

            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(x => x.AddProfile<ProductProfile>());
            builder.Services.AddAutoMapper(x => x.AddProfile<BasketProfile>());
            builder.Services.AddAutoMapper(x => x.AddProfile<OrderProfile>());


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

            builder.Services.AddDbContext<SecurityContext>(option =>
            {
               option.UseSqlServer(  builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<SecurityContext>();
            builder.Services.AddScoped<IAuthenticationServ, AuthenticationServ>();
            builder.Services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.SaveToken = true;

                opt.TokenValidationParameters = new TokenValidationParameters()
                {

                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWTOptions:Issuer"],
                    ValidAudience = builder.Configuration["JWTOptions:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTOptions:SecretKey"]))
                };

            });
            builder.Services.AddScoped<IOrderServices, OrderServices>();

            #endregion


            var app = builder.Build();
            #region Dataseed
            await app.migrateDataSeeding();
            await app.DataSeeding();
            await app.DataSeedingIdentity();
            #endregion

            #region Configure the HTTP request pipeline.


            app.UseMiddleware<ExceptionHandlerMiddleWear>();
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
