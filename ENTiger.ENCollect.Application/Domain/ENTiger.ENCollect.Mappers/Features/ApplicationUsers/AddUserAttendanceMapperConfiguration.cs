using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddUserAttendanceMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public AddUserAttendanceMapperConfiguration() : base()
        {
            CreateMap<AddUserAttendanceDto, UserAttendanceLog>();
        }
    }
}