using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserPerformanceBandMasterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UserPerformanceBandMasterMapperConfiguration() : base()
        {
            CreateMap<UserPerformanceBandMasterDto, UserPerformanceBandMaster>();
            CreateMap<UserPerformanceBandMaster, UserPerformanceBandMasterDto>();
            CreateMap<UserPerformanceBandMasterDtoWithId, UserPerformanceBandMaster>();
            CreateMap<UserPerformanceBandMaster, UserPerformanceBandMasterDtoWithId>();
        }
    }
}