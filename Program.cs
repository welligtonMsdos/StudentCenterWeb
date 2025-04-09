using Microsoft.AspNetCore.Authentication.Cookies;
using StudentCenterWeb.Interfaces;
using StudentCenterWeb.Services;
using StudentCenterWeb.Util;

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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader());
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(options =>
           {
               options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
               options.SlidingExpiration = true;
               options.AccessDeniedPath = "/Home/Index";
           });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSignalR();

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

            app.UseCors("AllowAll");

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
            
            app.MapHub<StatusHub>("/statusHub");

            app.Run();            
        }
    }
}
