using ENTiger.ENCollect.AccountsModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect.Mappers.Features.Accounts
{
    public partial class TeleCallerBirdEyeViewMapperConfiguration : FlexMapperProfile
    {
        public TeleCallerBirdEyeViewMapperConfiguration()
        {
            CreateMap<LoanAccount, BirdEyeViewAccountDetailDto>()
              .ForMember(d => d.TeleCallingAgency, s => s.MapFrom(a => a.TeleCallingAgency.FirstName))
              .ForMember(d => d.Agency, s => s.MapFrom(a => a.Agency.FirstName))
              .ForMember(d => d.Collector, s => s.MapFrom(a => a.Collector.FirstName + " " + a.Collector.LastName))
              .ForMember(d => d.TeleCaller, s => s.MapFrom(a => a.TeleCaller.FirstName + " " + a.TeleCaller.LastName))
              ;
        }
    }
}