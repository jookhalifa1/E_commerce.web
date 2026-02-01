using E_commerce.Presistence.Data.DBContexts;
using E_Commerce.Domain.Contract.DataIdentifier;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace E_commerce.web.Extenstions
{
    public static class Web
    {
        public static async Task<WebApplication> migrateDataSeeding( this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();

            var dbcontext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
            var pendingMigration = await dbcontext.Database.GetPendingMigrationsAsync();
            if ( pendingMigration.Any())
                await dbcontext.Database.MigrateAsync();
            return app; 
        }
        public static async Task<WebApplication> DataSeeding(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            var dataseed = scope.ServiceProvider.GetRequiredService<Idataseeding>();
            await dataseed.insilizeAsync();
            return app;
        }

    }
}
