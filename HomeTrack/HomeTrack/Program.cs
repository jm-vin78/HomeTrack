using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EntryPoint;
using HomeTrack.CLI;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace HomeTrack
{
    public class Program
    {
        public static void Main(string[] args)
        {

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var arguments = Cli.Parse<HomeTrackArguments>(args);

            return WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(builder =>
                {
                    var configPath = String.IsNullOrEmpty(arguments.ConfigPath)
                        ? Path.Combine(Assembly.GetExecutingAssembly().Location, "Config.yaml")
                        : arguments.ConfigPath;
                 
                    Console.WriteLine($"Using config from path: {configPath}");
                    builder.AddYamlFile(configPath);
                })
                .UseStartup<Startup>();
        }
    }
}
