using Sumeru.Flex;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetMyOpenSettlementsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        /// 
        /// </summary>
        public GetMyOpenSettlementsMapperConfiguration() : base()
        {
            CreateMap<Settlement, GetMyOpenSettlementsDto>();

        }
    }
}
