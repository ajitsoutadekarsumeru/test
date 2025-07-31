namespace ENTiger.ENCollect
{
    public class SendMobileForgotPasswordDto : DtoBridge
    {
        public string? Code { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}