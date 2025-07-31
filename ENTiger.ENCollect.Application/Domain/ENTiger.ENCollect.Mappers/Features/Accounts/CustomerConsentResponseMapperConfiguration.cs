using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class CustomerConsentResponseMapperConfiguration : FlexMapperProfile
    {
        public CustomerConsentResponseMapperConfiguration() : base()
        {
            CreateMap<CustomerConsentResponseDto, CustomerConsent>()
                .ForMember(d => d.Id, s => s.MapFrom(a => a.ConsentId))
                .ForMember(d => d.Status, s => s.MapFrom(a => a.Action))
                ;

        }
    }
}
