using Microsoft.AspNetCore.Identity;
using TriftyTrifty.DataAccess.Models;

namespace TriftyTrifty.Data
{
    public static class SeedRoles
    {
        public static async Task SeedAdminAsync(UserManager<AppUser> userManager)
        {
            var email = "admin@trifty.com";
            var admin = await userManager.FindByEmailAsync(email);

            if (admin == null)
            {
                admin = new AppUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
