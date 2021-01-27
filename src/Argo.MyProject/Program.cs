using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using PostSharp.Patterns.Diagnostics;
using CustomNLogBackend = Argo.MyProject.Logging.CustomNLogBackend;
using LogLevel = NLog.LogLevel;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Argo.MyProject
{
    // It is important that this attribute appear on any public method or class that should not have generated logging and is not excluded by policy.
    [Log(AttributeExclude = true)]  
    public class Program
    {
        public static void Main(string[] args)
        {
            // This enables NLog logging.  This should be done first.
            LogManager.EnableLogging();  
            // Since we have a custom backend, we assign it as the default for PostSharp
            LoggingServices.DefaultBackend = new CustomNLogBackend();
            // This is optional and could be set through configuration
            LoggingServices.DefaultBackend.DefaultVerbosity.SetMinimalLevel(PostSharp.Patterns.Diagnostics.LogLevel.Trace);
            LoggingServices.DefaultBackend.Options.IncludeActivityExecutionTime = true;
            // Get an instance of NLog for logging in the Program.cs file.
            var logger = LogManager.GetCurrentClassLogger();

            try
            {
                logger.Debug("Init main");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                logger.Log(LogLevel.Fatal, exception);
            }
            finally
            {
                LogManager.Shutdown();
            }
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel((context, options) =>
                    {
                        // https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.server.kestrel.core.kestrelserverlimits.minrequestbodydatarate?view=aspnetcore-3.1
                        options.Limits.MinRequestBodyDataRate = null;
                    });
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();  // NLog: Setup NLog for Dependency injection

    }
}
