using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class NativeWriteDbTenantConnectionProvider : FlexNativeWriteDbTenantConnectionProvider<FlexTenantBridge, FlexAppContextBridge>, IWriteDbConnectionProviderBridge
    {
        public NativeWriteDbTenantConnectionProvider(IFlexNativeHostTenantProviderBridge flexTenantProvider) : base(flexTenantProvider)
        {
        }
    }
}