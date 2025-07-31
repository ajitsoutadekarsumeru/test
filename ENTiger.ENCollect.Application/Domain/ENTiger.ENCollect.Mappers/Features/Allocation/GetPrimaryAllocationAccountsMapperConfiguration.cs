using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetPrimaryAllocationAccountsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetPrimaryAllocationAccountsMapperConfiguration() : base()
        {
            CreateMap<LoanAccount, GetPrimaryAllocationAccountsDto>()
             .ForMember(d => d.AccountNumber, s => s.MapFrom(a => a.AGREEMENTID))
             .ForMember(d => d.CustomerNumber, s => s.MapFrom(a => a.CUSTOMERID))
             .ForMember(d => d.SchemeCode, s => s.MapFrom(a => a.PRODUCT))
             .ForMember(d => d.CommunicationCityCode, s => s.MapFrom(a => a.CITY))
             .ForMember(d => d.Alpha, s => s.MapFrom(a => a.BRANCH))
             .ForMember(d => d.Region, s => s.MapFrom(a => a.Region))
             .ForMember(d => d.Zone, s => s.MapFrom(a => a.ZONE))
             .ForMember(d => d.TotalOverdue, s => s.MapFrom(a => a.TOS))
             .ForMember(d => d.NPAFlag, s => s.MapFrom(a => a.NPA_STAGEID))
             .ForMember(d => d.AllocationOwnerName, s => s.MapFrom(a => a.AllocationOwner.FirstName))
             .ForMember(d => d.TCallingAgencyName, s => s.MapFrom(a => a.TeleCallingAgency.FirstName))
             .ForMember(d => d.TCallingAgentName, s => s.MapFrom(a => a.TeleCaller.FirstName))
             .ForMember(d => d.AgencyName, s => s.MapFrom(a => a.Agency.FirstName))
             .ForMember(d => d.AgentName, s => s.MapFrom(a => a.Collector.FirstName))
             ;
        }
    }
}