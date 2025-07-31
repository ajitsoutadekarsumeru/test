using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateUserAttendanceMapperConfiguration : FlexMapperProfile
    {
        /// <summary>
        ///
        /// </summary>
        public UpdateUserAttendanceMapperConfiguration() : base()
        {
            CreateMap<UpdateUserAttendanceDto, UserAttendanceLog>();
        }
    }
}