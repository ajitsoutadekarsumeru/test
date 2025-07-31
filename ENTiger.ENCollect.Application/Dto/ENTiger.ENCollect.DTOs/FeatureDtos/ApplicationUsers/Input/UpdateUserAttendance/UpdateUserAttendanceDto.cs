namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class UpdateUserAttendanceDto : DtoBridge
    {
        public DateTime? LogOutTime { get; set; }
        public double? LogOutLatitude { get; set; }
        public double? LogOutLongitude { get; set; }
    }
}