using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ENTiger.ENCollect
{
    public class ConfigureLogger
    {
        public static LoggerConfiguration ForSerilog(HostBuilderContext context, LoggerConfiguration loggerConfig, string hostName, IConfiguration configuration)
        {
            if (context.HostingEnvironment.IsDevelopment())
            {
                loggerConfig = loggerConfig
                .ReadFrom.Configuration(context.Configuration)  // minimum levels defined per project in json files
                .Enrich.WithProperty("ApplicationName", hostName);
            }
            if (context.HostingEnvironment.IsStaging())
            {
                loggerConfig
                .ReadFrom.Configuration(context.Configuration) // minimum levels defined per project in json files
                .Enrich.WithProperty("ApplicationName", hostName);                                                 //
            }
            if (context.HostingEnvironment.IsProduction())
            {
                loggerConfig
                .ReadFrom.Configuration(context.Configuration) // minimum levels defined per project in json files
                .Enrich.WithProperty("ApplicationName", hostName);
            }
            return loggerConfig;
        }
    }
}