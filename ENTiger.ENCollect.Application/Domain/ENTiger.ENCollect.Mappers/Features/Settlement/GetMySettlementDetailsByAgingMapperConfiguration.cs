using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMySettlementDetailsByAgingMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetMySettlementDetailsByAgingMapperConfiguration() : base()
        {
            CreateMap<Settlement, GetMySettlementDetailsByAgingDto>();

        }
    }
}
