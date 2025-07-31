using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class GetCustomerConsentByTokenMapperConfiguration : FlexMapperProfile
    {
        public GetCustomerConsentByTokenMapperConfiguration() : base()
        {
            CreateMap<CustomerConsent, GetCustomerConsentByTokenDto>()
                .ForMember(d => d.ConsentId, s => s.MapFrom(a => a.Id))
                .ForMember(d => d.RequestedVisitTime, s => s.MapFrom(a => a.RequestedVisitTime))
                .ForMember(d => d.UserId, s => s.MapFrom(a => a.UserId))
                .ForMember(d => d.ExpiryDateTime, s => s.MapFrom(a => a.ExpiryTime))
                .ForMember(d => d.AccountId, s => s.MapFrom(a => a.AccountId))
                .ForMember(d => d.ConsentSentDate, s => s.MapFrom(a => a.CreatedDate.DateTime))
                .ForMember(d => d.IsActive, s => s.MapFrom(a => a.IsActive))
                .ForMember(d => d.Status, s => s.MapFrom(a => a.Status))
                ; 
        }
    }
}
