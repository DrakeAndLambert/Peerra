using System;
using DrakeLambert.Peerra.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DrakeLambert.Peerra
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var environment = services.GetRequiredService<IHostingEnvironment>();

                    // TODO: Figure out real DB situation
                    // if (!environment.IsDevelopment())
                    // {
                    //     var context = services.GetRequiredService<ApplicationDbContext>();
                    //     context.Database.Migrate();
                    // }
                    var seed = services.GetRequiredService<InitialDataSeed>();
                    seed.Seed();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred initializing the DB.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(options => 
                {
                    options.AddAzureWebAppDiagnostics();
                })
                .UseStartup<Startup>();
    }
}
