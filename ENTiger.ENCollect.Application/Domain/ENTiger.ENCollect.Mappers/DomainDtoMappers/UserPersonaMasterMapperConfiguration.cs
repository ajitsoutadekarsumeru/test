using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserPersonaMasterMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UserPersonaMasterMapperConfiguration() : base()
        {
            CreateMap<UserPersonaMasterDto, UserPersonaMaster>();
            CreateMap<UserPersonaMaster, UserPersonaMasterDto>();
            CreateMap<UserPersonaMasterDtoWithId, UserPersonaMaster>();
            CreateMap<UserPersonaMaster, UserPersonaMasterDtoWithId>();
        }
    }
}