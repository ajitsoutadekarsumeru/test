namespace ENTiger.ENCollect.CommonModule
{
    public partial class SendOTPToVerifyNumberDto : DtoBridge
    {
        public string? PhoneNo { get; set; }
        public string? UserId { get; set; }
        public string? Description { get; set; }
        public string? CustomerName { get; set; }
    }
}