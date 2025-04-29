using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pronia.DAL;

namespace Pronia
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(opt=>opt.UseSqlServer("server=WIN-4AE5BBJV6NS\\SQLEXPRESS;database=ProniaDb;trusted_connection=true;integrated security=true; TrustServerCertificate=true"));
            var app = builder.Build();
            app.UseStaticFiles();
            app.MapControllerRoute(
                "default",
                "{controller=home}/{action=index}/{Id?}"

                );

            app.Run();
        }
    }
}
