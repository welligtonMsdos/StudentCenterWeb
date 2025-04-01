using StudentCenterWeb.Interfaces;
using StudentCenterWeb.Services;
using StudentCenterWeb.Util;
using System.Net.WebSockets;

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

            // Add services to the container.
            builder.Services.AddControllersWithViews();

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

            //async Task HandleWebSocketConnection(WebSocket webSocket)
            //{
            //    var buffer = new byte[1024 * 4];
            //    WebSocketReceiveResult result;
            //    do
            //    {
            //        result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            //        await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);
            //    } while (!result.CloseStatus.HasValue);

            //    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
            //}
        }
    }
}
