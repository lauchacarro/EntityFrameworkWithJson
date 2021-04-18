using EntityFrameworkWithJson.Data;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using System.IO;

namespace EntityFrameworkWithJson
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string dbName = "TestDatabase.db";


            using (var dbContext = new ApplicationDbContext())
            {
                if (!File.Exists(dbName))
                {
                    dbContext.Database.EnsureCreated();
                }
            }

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
