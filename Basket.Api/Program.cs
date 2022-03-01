using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Winton.Extensions.Configuration.Consul;
using ILogger = Serilog.ILogger;

namespace Basket.Api
{
    public class Program
    {
        const string ConsulSection = "Consul";
        const string ConsulHost = "Host";
        public static void Main(string[] args)
        {
            var appConfiguration = GetAppConfiguration();
            Log.Logger = CreateLogger(appConfiguration);
            CreateHostBuilder(args,appConfiguration).Build().Run();
        }

        private static IConfiguration GetAppConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }

        private static ILogger CreateLogger(IConfiguration appConfiguration)
        {
            var loggerConfiguration = new LoggerConfiguration();
            return loggerConfiguration.CreateLogger();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args, IConfiguration appConfiguration) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>().ConfigureAppConfiguration(builder =>
                {
                    builder.AddConsul(
                        "config/basket-service",
                        options =>
                        {
                            options.ConsulConfigurationOptions =
                                cco =>
                                {
                                    cco.Address = new Uri(appConfiguration.GetSection(ConsulSection)[ConsulHost]);
                                };
                            //options.Optional = true;
                            options.PollWaitTime = TimeSpan.FromSeconds(5);
                            options.ReloadOnChange = true;
                        }).AddEnvironmentVariables();
                });
        //.UseKestrel(((context, options) =>
        //{
        //   options.ListenAnyIP(5000);
        //}));
    }
}
