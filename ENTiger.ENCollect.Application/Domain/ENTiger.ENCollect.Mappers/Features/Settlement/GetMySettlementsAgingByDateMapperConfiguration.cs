using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMySettlementsAgingByDateMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetMySettlementsAgingByDateMapperConfiguration() : base()
        {
            CreateMap<Settlement, GetMySettlementsAgingByDateDto>();

        }
    }
}
