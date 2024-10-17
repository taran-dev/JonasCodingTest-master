using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static ILogger<WebApiApplication> _logger;

        protected void Application_Start()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.File("logs/log.txt")
                .CreateLogger();

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddSerilog();
            });

            // Create a logger instance
            _logger = loggerFactory.CreateLogger<WebApiApplication>();

            _logger.LogInformation("Application Start");

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
