namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class AddUserAttendanceDto : DtoBridge
    {
        public string? SessionId { get; set; }
        public DateTime LogInTime { get; set; }
        public double? LogInLatitude { get; set; }
        public double? LogInLongitude { get; set; }
        public string? Token { get; set; }
    }
}