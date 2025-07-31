using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class GetCustomerConsentListMapperConfiguration : FlexMapperProfile
    {
        public GetCustomerConsentListMapperConfiguration() : base()
        {
            CreateMap<CustomerConsent, GetCustomerConsentListDto>()
                 .ForMember(d => d.AccountNo, s => s.MapFrom(a => a.Account.AGREEMENTID))
                 .ForMember(d => d.ConsentId, s => s.MapFrom(a => a.Id))
                 .ForMember(d => d.UserId, s => s.MapFrom(a => a.User.CustomId))
                 .ForMember(d => d.CreatedDateTime, s => s.MapFrom(a => a.CreatedDate.DateTime));

        }
    }
}
