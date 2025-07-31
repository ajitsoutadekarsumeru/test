namespace ENTiger.ENCollect
{
    public class SendForgotPasswordDto : DtoBridge
    {
        public string Email { get; set; }
        public string Code { get; set; }
    }
}