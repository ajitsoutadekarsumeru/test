using Sumeru.Flex;
using System.Linq;

namespace ENTiger.ENCollect
{
    public class FlexNativeHostTenantProviderBridge : FlexNativeHostTenantProvider<FlexTenantBridge, FlexAppContextBridge>, IFlexNativeHostTenantProviderBridge
    {
        public FlexNativeHostTenantProviderBridge(IFlexTenantRepository<FlexTenantBridge> flexTenantRepository) : base(flexTenantRepository)
        {
        }

        public override FlexTenantBridge GetTenant(FlexAppContextBridge hostContextInfo)
        {
            //return base.GetTenant(hostContextInfo);
            var tenantId = hostContextInfo.TenantId;
            var tenant =  _flexTenantRepository.FindAllTenants().Where(t => t.Id == tenantId).FirstOrDefault();
            return tenant;
        }
    }
}
