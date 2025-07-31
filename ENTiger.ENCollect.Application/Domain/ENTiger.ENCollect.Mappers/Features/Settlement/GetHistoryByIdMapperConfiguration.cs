using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetHistoryByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetHistoryByIdMapperConfiguration() : base()
        {
            CreateMap<SettlementStatusHistory, GetHistoryByIdDto>();
            CreateMap<List<SettlementStatusHistory>, GetHistoryByIdDto>()
                    .ForMember(dest => dest.WorkflowHistory, opt => opt.MapFrom(src => src));

            CreateMap<SettlementStatusHistory, WorkFlowHistoryDto>()
                    .ForMember(dest => dest.StatusUpdatedDate, opt => opt.MapFrom(src => src.ChangedDate))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ToStatus))
                    .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Comment))
                    .ForMember(dest => dest.RejectionReason, opt => opt.MapFrom(src => src.Comment))
                    .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.ChangedByUserId)); 
        }
    }
}
