using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserAccessRightsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UserAccessRightsMapperConfiguration() : base()
        {
            CreateMap<UserAccessRightsDto, UserAccessRights>();
            CreateMap<UserAccessRights, UserAccessRightsDto>();
            CreateMap<UserAccessRightsDtoWithId, UserAccessRights>();
            CreateMap<UserAccessRights, UserAccessRightsDtoWithId>();
        }
    }
}