using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class RequestCustomerConsentMapperConfiguration : FlexMapperProfile
    {
        public RequestCustomerConsentMapperConfiguration() : base()
        {
            CreateMap<RequestCustomerConsentDto, CustomerConsent>();

        }
    }
}
