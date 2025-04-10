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

            var uriAuth = builder.Configuration["ServiceUrls:StudentCenterAuthAPI"];

            if (string.IsNullOrEmpty(uri))
            {
              throw new ArgumentNullException(nameof(uri), "The URI string cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(uriAuth))
            {
                throw new ArgumentNullException(nameof(uri), "The URI string cannot be null or empty.");
            }

            builder.Services.AddHttpClient<IStudentCenterService, StudentCenterService>(c =>
            c.BaseAddress = new Uri(uri));

            builder.Services.AddHttpClient<IStudentCenterAuthService, StudentCenterAuthService>(c => c.BaseAddress = new Uri(uriAuth));

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
               options.AccessDeniedPath = "/Account/Login";
           });

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSignalR();

            var app = builder.Build();

            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 401) // Verifica se o status é Unauthorized
                {
                    context.Response.Redirect("/Account/Login"); // Redireciona para a tela de login
                }
            });

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
                pattern: "{controller=Account}/{action=Login}/{id?}");
            
            app.MapHub<StatusHub>("/statusHub");

            app.Run();            
        }
    }
}
