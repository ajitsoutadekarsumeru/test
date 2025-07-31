using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMyClosedSettlementsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetMyClosedSettlementsMapperConfiguration() : base()
        {
            CreateMap<Settlement, GetMyClosedSettlementsDto>();

        }
    }
}
