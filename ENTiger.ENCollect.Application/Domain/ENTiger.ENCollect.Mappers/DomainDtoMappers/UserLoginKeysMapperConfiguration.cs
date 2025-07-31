using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserLoginKeysMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UserLoginKeysMapperConfiguration() : base()
        {
            CreateMap<UserLoginKeysDto, UserLoginKeys>();
            CreateMap<UserLoginKeys, UserLoginKeysDto>();
            CreateMap<UserLoginKeysDtoWithId, UserLoginKeys>();
            CreateMap<UserLoginKeys, UserLoginKeysDtoWithId>();
        }
    }
}