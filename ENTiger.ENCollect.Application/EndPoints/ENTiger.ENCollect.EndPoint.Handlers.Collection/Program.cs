using ENTiger.ENCollect;
using Sumeru.Flex;

System.Diagnostics.Trace.WriteLine($"Collection");
IHost host = Host.CreateDefaultBuilder(args).UseWindowsService()
        .ConfigureCommonHost
            (
                new ConfigureEndPointHostParams
                {
                    HostName = "ENTiger.Collection.Endpoint",
                    Routes = NsbRouteConfig.GetRoute(),
                    SearchPattern = "ENTiger.ENCollect*.dll"
                }
            )
        .ConfigureServices((hostingContext, services) =>
            {
                services.AddHostedService<Worker>();
                services.AddSingleton<MessageTemplateFactory>();
                services.AddSingleton<EmailProviderFactory>();
                services.AddSingleton<SmsProviderFactory>();
                services.AddSingleton<PaymentGatewayFactory>();
                services.AddSingleton<PackageSSISProviderFactory>();
                services.AddSingleton<ADAuthProviderFactory>();

                services.AddTransient<PaymentGatewayPaynimo>();
                services.AddTransient<PaymentGatewayRazorPay>();
                services.AddTransient<PaymentGatewayPayu>();
                services.AddTransient<SmsProviderKarix>();
                services.AddTransient<SmsProvider24X7>();
                services.AddTransient<SmsProviderInfoBip>();
                services.AddTransient<EmailProviderSmtp>();
                services.AddTransient<EmailProviderNetCoreCloud>();
                services.AddTransient<ISmsUtility, SmsUtility>();
                services.AddTransient<IEmailUtility, EmailUtility>();
                services.AddTransient<LDAPAuthProvider>();
                services.AddExtensions();            
                })
    .Build();

var serviceScope = host.Services.CreateScope();

var serviceProvider = serviceScope.ServiceProvider;
var flexHost = serviceProvider.GetRequiredService<IFlexHost>();
flexHost.SetServiceProvider(serviceProvider);

var endpoint = FlexContainer.ServiceProvider.GetRequiredService<IConfiguration>().GetSection("EndPoint")["Name"];
System.Diagnostics.Trace.WriteLine(endpoint);

await host.RunAsync();