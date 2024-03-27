using System.Collections.Immutable;

using DivMaps.Models;
using DivMaps.Services;

namespace DivMaps
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<DataContainer>();

            builder.Services.AddHostedService<UpdateBackgroundService>();

            WebApplication app = builder.Build();

            //// Configure the HTTP request pipeline.
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //}

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseAuthorization();

            //app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapControllers();

            app.Run();
        }
    }
}
