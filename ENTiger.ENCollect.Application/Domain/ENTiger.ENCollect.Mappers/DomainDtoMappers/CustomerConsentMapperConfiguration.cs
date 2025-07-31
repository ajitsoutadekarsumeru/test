using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CustomerConsentMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public CustomerConsentMapperConfiguration() : base()
        {
            CreateMap<CustomerConsentDto, CustomerConsent>();
            CreateMap<CustomerConsent, CustomerConsentDto>();
            CreateMap<CustomerConsentDtoWithId, CustomerConsent>();
            CreateMap<CustomerConsent, CustomerConsentDtoWithId>();

        }
    }
}
