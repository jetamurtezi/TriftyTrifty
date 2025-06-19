using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TriftyTrifty.DataAccess.Data;
using TriftyTrifty.DataAccess.Models;
using TriftyTrifty.DataAccess.Repositories;
using TriftyTrifty.DataAccess.Repositories.IRepositories;

namespace TriftyTrifty
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1. Konfigurimi i DbContext dhe Identity
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddIdentity<AppUser, IdentityRole>()
                   .AddEntityFrameworkStores<AppDbContext>()
                   .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Identity/Account/Login";
                opt.AccessDeniedPath = "/Identity/Account/AccessDenied";
            });

            // 2. Shtimi i repositories
            builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
            builder.Services.AddScoped<IExpenseGroupRepository, ExpenseGroupRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // 3. Krijimi i roleve dhe përdoruesit admin në start
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var roleMgr = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userMgr = services.GetRequiredService<UserManager<AppUser>>();

                await SeedRolesAsync(roleMgr);
                await SeedAdminAsync(userMgr);
            }

            // 4. Middleware pipeline
            if (!app.Environment.IsDevelopment())
                app.UseExceptionHandler("/Home/Error");

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            await app.RunAsync();
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("User"))
                await roleManager.CreateAsync(new IdentityRole("User"));
        }

        private static async Task SeedAdminAsync(UserManager<AppUser> userManager)
        {
            var email = "admin@trifty.com";
            var admin = await userManager.FindByEmailAsync(email);

            if (admin == null)
            {
                admin = new AppUser
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    Name = "Administrator"
                };

                var result = await userManager.CreateAsync(admin, "Admin123!");
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
