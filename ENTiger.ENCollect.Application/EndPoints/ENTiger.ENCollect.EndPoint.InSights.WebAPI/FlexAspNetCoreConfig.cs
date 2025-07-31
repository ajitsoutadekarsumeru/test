using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public static class FlexAspNetCoreConfig
    {
        public static void AddFlexBaseAspNetCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IFlexHostHttpContextAccesorBridge, FlexDefaultHttpContextAccessorBridge>();
            services.AddTransient<IApplicationUserUtility, ApplicationUserUtility>();

            services.AddAutoMapper(typeof(CoreMapperConfiguration).Assembly, typeof(HttpContextInfoMapperConfig).Assembly);

            services.AddFlexHttpCorrelationServices();
        }
    }
}

