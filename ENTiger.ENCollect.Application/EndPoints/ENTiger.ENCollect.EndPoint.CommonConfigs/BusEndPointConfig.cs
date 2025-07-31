using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class BusEndPointConfig
    {
        public static void AddFlexBaseBusServices(this IServiceCollection services)
        {
            //List<BusRouteConfig> routes = ConfigureBusRoute();
            services.AddSingleton<IFlexServiceBusBridge, NsbServiceBusBridge>();
            services.AddTransient<FlexBusSendOptions, NsbSendOptions>();
        }

        public static EndpointConfiguration GetEndpoint(IConfiguration configuration, IHostEnvironment env, List<BusRouteConfig> routes)
        {
            //List<BusRouteConfig> routes = ConfigureBusRoute();

            Guard.AgainstNullAndEmpty("End point name cannot be empty", configuration.GetSection("EndPoint")["Name"]);
            string endPointName = configuration.GetSection("EndPoint")["Name"];

            //You can change your Bus implementation here

            //Default configuration will remain learning transport if nothing is set.
            NsbDefaultEndpointConfiguration endPointConfig = new LearningNsbConfiguration(endPointName, routes, configuration, env);

            if (env.IsDevelopment())
            {
                endPointConfig = new LearningNsbConfiguration(endPointName, routes, configuration, env);
                //endPointConfig = new RabbitMqNsbConfiguration(endPointName, routes, configuration, env);
            }
            else if (env.IsStaging())
            {
                endPointConfig = new RabbitMqNsbConfiguration(endPointName, routes, configuration, env);
            }
            else if (env.IsProduction())
            {
                endPointConfig = new RabbitMqNsbConfiguration(endPointName, routes, configuration, env);
            }
            else
            {
                endPointConfig = new RabbitMqNsbConfiguration(endPointName, routes, configuration, env);
            }

            return endPointConfig;
        }
    }
}