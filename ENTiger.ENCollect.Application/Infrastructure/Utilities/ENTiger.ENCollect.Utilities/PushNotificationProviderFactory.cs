using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect;

public class PushNotificationProviderFactory : IFlexUtilityService
{
    private readonly string _pushProvider;
    private readonly NotificationSettings _settings;
    private readonly IServiceProvider _serviceProvider;

    public PushNotificationProviderFactory(IServiceProvider serviceProvider, IOptions<NotificationSettings> settings)
    {
        _serviceProvider = serviceProvider;
        _settings = settings.Value;
        _pushProvider = _settings.PushService;
    }

    public virtual IPushNotificationProvider GetPushNotificationProvider()
    {
        return _pushProvider.ToLower() switch
        {
            "firebase" => _serviceProvider.GetRequiredService<FirebasePushProvider>(),          
            _ => throw new InvalidOperationException("Invalid push notification provider type")
        };
    }
}
