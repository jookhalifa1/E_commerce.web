using E_Commerce.Domain.Contract.DataIdentifier;
using E_Commerce.Domain.Entity.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Presistence.Datase.IdintityDataseeding
{
    public class IdentitySeed : Idataseeding
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ILogger logger;

        public IdentitySeed(UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            ILogger<IdentitySeed> logger)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.logger = logger;
        }            
        
        public async Task insilizeAsync()
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));

                }
                if (!userManager.Users.Any())
                {
                    var user01 = new ApplicationUser()
                    {
                        DisplayName = "Youseef Sayed",
                        UserName = "YouseefSayed",
                        Email = "Youseef@gmail.com",
                        PhoneNumber = "01234556789"
                    };
                    var user02 = new ApplicationUser()
                    {
                        DisplayName = "Ahemd Sayed",
                        UserName = "AhmedSayed",
                        Email = "Ahmed@gmail.com",
                        PhoneNumber = "01234556789"
                    };
                    await userManager.CreateAsync(user01, "P@ssw0rd");
                    await userManager.CreateAsync(user02, "P@ssw0rd");

                    await userManager.AddToRoleAsync(user01, "Admin");
                    await userManager.AddToRoleAsync(user02, "SuperAdmin");
                }
            }
            catch(Exception ex)
            {
                logger.LogError($"Error While Seeding Identity :Message={ex.Message}");
            }
        }
    }
}
