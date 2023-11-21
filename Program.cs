using Microsoft.EntityFrameworkCore;
using TaskMngr.Data;

using TaskMngr.Service;
using TaskMngr.Interface;


namespace TaskMngr
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // connect DB

            string connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services
                .AddDbContext<TaskDb>
                (opions => opions.UseSqlServer(connString), ServiceLifetime.Scoped);


            // Configuring dependency injection: Associates ITsk with TskService using transient lifetime.
            builder.Services.AddTransient<ITsk, TskService>();

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
