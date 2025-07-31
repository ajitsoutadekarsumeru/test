using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserPerformanceBandMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UserPerformanceBandMapperConfiguration() : base()
        {
            CreateMap<UserPerformanceBandDto, UserPerformanceBand>();
            CreateMap<UserPerformanceBand, UserPerformanceBandDto>();
            CreateMap<UserPerformanceBandDtoWithId, UserPerformanceBand>();
            CreateMap<UserPerformanceBand, UserPerformanceBandDtoWithId>();
        }
    }
}