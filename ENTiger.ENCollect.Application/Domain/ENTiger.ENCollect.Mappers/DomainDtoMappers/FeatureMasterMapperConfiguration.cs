using Sumeru.Flex;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class FeatureMasterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public FeatureMasterMapperConfiguration() : base()
        {
            CreateMap<FeatureMasterDto, FeatureMaster>();
            CreateMap<FeatureMaster, FeatureMasterDto>();
            CreateMap<FeatureMasterDtoWithId, FeatureMaster>();
            CreateMap<FeatureMaster, FeatureMasterDtoWithId>();
        }
    }
}