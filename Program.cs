using StudentCenterWeb.Interfaces;
using StudentCenterWeb.Services;

namespace StudentCenterWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var uri = builder.Configuration["ServiceUrls:StudentCenterAPI"];

            if (string.IsNullOrEmpty(uri))
            {
              throw new ArgumentNullException(nameof(uri), "The URI string cannot be null or empty.");
            }

            builder.Services.AddHttpClient<IStudentCenterService, StudentCenterService>(c =>
            c.BaseAddress = new Uri(uri));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

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

            app.MapControllerRoute
            (
                name: "solicitation",
                pattern: "mySolicitation/{id?}",
                defaults: new { controller = "StudentCenter", action = "Solicitation" }
            );

            app.MapControllerRoute
            (
                name: "studentCenter",
                pattern: "studentCenter",
                defaults: new { controller = "StudentCenter", action = "GetAll" }
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
           
            app.Run();
        }
    }
}
