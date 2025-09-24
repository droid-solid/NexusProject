using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResolveIQ.Web.Data;
using ResolveIQ.Web.Data.Auth;
using ResolveIQ.Web.Data.Seeder;
using System.Reflection;
using static System.Formats.Asn1.AsnWriter;

namespace ResolveIQ.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), mySqlOptins => mySqlOptins.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName)));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddControllersWithViews();            

            var app = builder.Build();
            var serviceProvider = app.Services.CreateScope().ServiceProvider;

            using (var context = serviceProvider.GetRequiredService<ApplicationDbContext>()) 
            { 
                context.Database.Migrate();
                // Seed roles and users
                context.SeedRolesAndUsersAsync(
                    serviceProvider.GetRequiredService<UserManager<AppUser>>(),
                    serviceProvider.GetRequiredService<RoleManager<IdentityRole>>()
                ).GetAwaiter().GetResult();
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
