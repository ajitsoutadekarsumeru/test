using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserCurrentLocationDetailsMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UserCurrentLocationDetailsMapperConfiguration() : base()
        {
            CreateMap<UserCurrentLocationDetailsDto, UserCurrentLocationDetails>();
            CreateMap<UserCurrentLocationDetails, UserCurrentLocationDetailsDto>();
            CreateMap<UserCurrentLocationDetailsDtoWithId, UserCurrentLocationDetails>();
            CreateMap<UserCurrentLocationDetails, UserCurrentLocationDetailsDtoWithId>();
        }
    }
}