namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public class LoginValidationDto : DtoBridge
    {
        public string? error { get; set; }
        public string? error_description { get; set; }
        public string? message { get; set; }
    }
}