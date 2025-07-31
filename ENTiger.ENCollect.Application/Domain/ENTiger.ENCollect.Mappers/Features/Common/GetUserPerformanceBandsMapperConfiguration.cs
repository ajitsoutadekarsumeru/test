using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetUserPerformanceBandsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public GetUserPerformanceBandsMapperConfiguration() : base()
        {
            CreateMap<UserPerformanceBandMaster, GetUserPerformanceBandsDto>();
        }
    }
}