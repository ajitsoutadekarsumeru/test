using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetSettlementByIdMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetSettlementByIdMapperConfiguration() : base()
        {
            CreateMap<Settlement, GetSettlementByIdDto>()
                .ForMember(dest => dest.CurrentBucket, opt => opt.MapFrom(src => src.CURRENT_BUCKET));
        }
    }
}
