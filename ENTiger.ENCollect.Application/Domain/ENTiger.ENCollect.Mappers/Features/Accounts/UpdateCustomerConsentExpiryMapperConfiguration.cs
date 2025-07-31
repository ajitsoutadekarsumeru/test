using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateCustomerConsentExpiryMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public UpdateCustomerConsentExpiryMapperConfiguration() : base()
        {
            CreateMap<UpdateCustomerConsentExpiryDto, CustomerConsent>();

        }
    }
}
