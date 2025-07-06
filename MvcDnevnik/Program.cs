using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
<<<<<<< HEAD
=======
using MvcDnevnik.Models;
>>>>>>> 8822f4abcd7cb05dbba53f4a283da55bb9cd3478
using MvcDnevnik.Data;
namespace MvcDnevnik
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MvcDnevnikContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MvcDnevnikContext") ?? throw new InvalidOperationException("Connection string 'MvcDnevnikContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                SeedData.Initialize(services);
            }
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.Run();
        }
    }
}
