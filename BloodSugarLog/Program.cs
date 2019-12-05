using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Compact;
using System;

namespace BloodSugarLog
{
    public class Program
    {
        public static void Main(string[] args)
        {
                Log.Logger = new LoggerConfiguration()
                            .Enrich.FromLogContext()
                            .WriteTo.Console(new CompactJsonFormatter())
                            .WriteTo.File(new CompactJsonFormatter(), "./logs/myapp.json")
                            .CreateLogger();

           


            try
            {
                Log.Information("Starting up");
                CreateWebHostBuilder(args).Build().Run();

            }catch(Exception ex)
            {
                Log.Fatal(ex, "Host terminated unxpectedly");
            }finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration(AppConfigureSetting)
                .UseStartup<Startup>();

        private static void AppConfigureSetting(WebHostBuilderContext ctx, IConfigurationBuilder builder)
        {
            // clean up the config
            builder.Sources.Clear();
            builder.AddJsonFile("appConfig.json").AddEnvironmentVariables();
        }
    }
}
