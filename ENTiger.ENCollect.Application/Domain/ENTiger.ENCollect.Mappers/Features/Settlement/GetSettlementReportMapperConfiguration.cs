using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetSettlementReportMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetSettlementReportMapperConfiguration() : base()
        {
            CreateMap<Settlement, SettlementReportDto>()
                 .ForMember(dest => dest.RequestorId, opt => opt.MapFrom(src => src.CreatedBy))
                 .ForMember(dest => dest.NpaFlag, opt => opt.MapFrom(src => src.NPA_STAGEID));
                

        }
    }
}
