using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SettlementMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public SettlementMapperConfiguration() : base()
        {
            CreateMap<SettlementDto, Settlement>();
            CreateMap<Settlement, SettlementDto>();
            CreateMap<SettlementDtoWithId, Settlement>();
            CreateMap<Settlement, SettlementDtoWithId>();
            CreateMap<Settlement, MySettlementSummaryDetailsDto>()
                .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => src.SettlementRemarks))
                .ForMember(dest => dest.AccountId, opt => opt.MapFrom(src => src.LoanAccountId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => SettlementStatusEnum.ByValue(src.Status).DisplayName));
        }
    }
}
