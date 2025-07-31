using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetWaiversByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetWaiversByIdMapperConfiguration() : base()
        {
            CreateMap<List<WaiverDetail>, GetWaiversByIdDto>()
                    .ForMember(dest => dest.WaiverDetails, opt => opt.MapFrom(src => src));

            CreateMap<WaiverDetail, WaiverDetailDto>();
        }
    }
}
