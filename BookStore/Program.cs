using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using BookStore.Models.Repositories;
using System.Configuration;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //string connString = builder.Configuration.GetConnectionString("SqlCon");

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IBookStoreRepository<Author>,AuthorDbRepository>();
            builder.Services.AddScoped<IBookStoreRepository<Book>,BookDbRepository>();
            builder.Services.AddDbContext<BookStoreDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SqlCon"));
            });
            var app = builder.Build();

            //RunMigrations(app);

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
                pattern: "{controller=Book}/{action=Index}/{id?}");

            app.Run();
        }
        //private static void RunMigrations(WebApplication app)
        //{
        //    using (var scope = app.Services.CreateScope())
        //    {
        //        var db = scope.ServiceProvider.GetRequiredService<BookStoreDbContext>();
        //        db.Database.Migrate();
        //    }
        //}
    }
}