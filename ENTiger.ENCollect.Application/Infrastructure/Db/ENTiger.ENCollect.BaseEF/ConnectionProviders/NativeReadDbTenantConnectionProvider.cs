using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NativeReadDbTenantConnectionProvider : FlexNativeReadDbTenantConnectionProvider<FlexTenantBridge, FlexAppContextBridge>, IReadDbConnectionProviderBridge
    {
        public NativeReadDbTenantConnectionProvider(IFlexNativeHostTenantProviderBridge flexTenantProvider) : base(flexTenantProvider)
        {
        }
    }
}