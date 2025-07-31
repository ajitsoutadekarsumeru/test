using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMySettlementsAgingByStatusMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetMySettlementsAgingByStatusMapperConfiguration() : base()
        {
            CreateMap<Settlement, GetMySettlementsAgingByStatusDto>();

        }
    }
}
