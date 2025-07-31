
using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Factory for resolving Tiny URL provider implementations based on provider type enum.
    /// </summary>
    public class TinyUrlFactory : IFlexUtilityService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<TinyUrlProviderTypeEnum, Type> _providerMappings;

        public TinyUrlFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _providerMappings = new Dictionary<TinyUrlProviderTypeEnum, Type>
            {
                // Add additional providers here
            };
        }

        public virtual ITinyUrlProvider GetTinyUrlProvider(TinyUrlProviderTypeEnum providerType)
        {
            if (_providerMappings.TryGetValue(providerType, out var providerClassType))
            {
                return (ITinyUrlProvider)_serviceProvider.GetRequiredService(providerClassType);
            }

            throw new InvalidOperationException($"No Tiny URL provider found for type: {providerType}");
        }
    }
}