using InterestRateApp.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InterestRateApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            MigrateDatabase(host);
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void MigrateDatabase(IHost host)
        {
            var scope = host.Services.CreateScope();
            using (scope)
            {
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("InterestRateAppDatabase");
                var baseOptions = scope.ServiceProvider.GetRequiredService<DbContextOptions<DatabaseContext>>();
                var options = new DbContextOptionsBuilder<DatabaseContext>(baseOptions).UseSqlServer(connectionString);

                using (var db = new DatabaseContext(options.Options))
                {
                    db.MigrateDatabase();
                }

            }
        }
    }
}
