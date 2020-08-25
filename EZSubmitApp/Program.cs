using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System;

namespace EZSubmitApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // NLog: Setup the logger first to catch all errors
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                // NLog: Catch setup errors
                logger.Error(ex, "Stopped program because of exception.");
                throw;
            }
            finally
            {
                // NLog: Make sure to flush & stop internal timers/threads before application-exit
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNLog(); // NLog: Setup NLog for Dependency Injection
    }
}
