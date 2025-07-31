using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class GetCustomerConsentByIdMapperConfiguration : FlexMapperProfile
    {
        public GetCustomerConsentByIdMapperConfiguration() : base()
        {
            CreateMap<CustomerConsent, GetCustomerConsentByIdDto>()
                .ForMember(d => d.ConsentId, s => s.MapFrom(a => a.Id))
                .ForMember(d => d.VisitRequestedDateTime, s => s.MapFrom(a => a.RequestedVisitTime))
                .ForMember(d => d.UserId, s => s.MapFrom(a => a.User.CustomId))
                .ForMember(d => d.ExpiryDateTime, s => s.MapFrom(a => a.ExpiryTime))
                .ForMember(d => d.AccountId, s => s.MapFrom(a => a.Account.AGREEMENTID))
                .ForMember(d => d.ConsentSentDate, s => s.MapFrom(a => a.CreatedDate.DateTime))
                .ForMember(d => d.ConsentStatusDate, s => s.MapFrom(a => a.ConsentResponseTime))
                .ForMember(d => d.IsActive, s => s.MapFrom(a => a.IsActive))
                .ForMember(d => d.Status, s => s.MapFrom(a => a.Status))
                ;

        }
    }
}
