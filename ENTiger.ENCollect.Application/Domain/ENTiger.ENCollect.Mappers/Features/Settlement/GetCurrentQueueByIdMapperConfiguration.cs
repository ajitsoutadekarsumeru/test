using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetCurrentQueueByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetCurrentQueueByIdMapperConfiguration() : base()
        {
            CreateMap<SettlementQueueProjection, GetCurrentQueueByIdDto>()
                .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.ApplicationUserId))
                .ForMember(dest => dest.AssignedAt, opt => opt.MapFrom(src => src.CreatedDate.UtcDateTime));

        }
    }
}
