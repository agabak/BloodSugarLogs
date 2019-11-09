using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BloodSugarLog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
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
