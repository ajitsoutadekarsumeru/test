using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserAttendanceLogMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UserAttendanceLogMapperConfiguration() : base()
        {
            CreateMap<UserAttendanceLogDto, UserAttendanceLog>();
            CreateMap<UserAttendanceLog, UserAttendanceLogDto>();
            CreateMap<UserAttendanceLogDtoWithId, UserAttendanceLog>();
            CreateMap<UserAttendanceLog, UserAttendanceLogDtoWithId>();
        }
    }
}