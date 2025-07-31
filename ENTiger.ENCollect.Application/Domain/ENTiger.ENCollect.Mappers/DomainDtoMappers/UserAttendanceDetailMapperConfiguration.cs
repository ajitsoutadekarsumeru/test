using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserAttendanceDetailMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UserAttendanceDetailMapperConfiguration() : base()
        {
            CreateMap<UserAttendanceDetailDto, UserAttendanceDetail>();
            CreateMap<UserAttendanceDetail, UserAttendanceDetailDto>();
            CreateMap<UserAttendanceDetailDtoWithId, UserAttendanceDetail>();
            CreateMap<UserAttendanceDetail, UserAttendanceDetailDtoWithId>();
        }
    }
}