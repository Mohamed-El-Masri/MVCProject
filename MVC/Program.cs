using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mvc.Models.Authorize;
using MVC.Models;

namespace mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
               options =>
               {
                   options.Password.RequireDigit = false;



                   options.Password.RequireLowercase = false;
                   options.Password.RequireUppercase = false;
                   options.Password.RequireNonAlphanumeric = false;
                   options.User.RequireUniqueEmail = true;



               }

               ).AddEntityFrameworkStores<ProjectContext>();


            //register services
            builder.Services.AddDbContext<ProjectContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
