namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public partial class VerifyLoginOTPDto : DtoBridge
    {
        public string? emailId { get; set; }

        public string Code { get; set; }

        public string? referenceId { get; set; }

        public string? phoneNumber { get; set; }
    }
}