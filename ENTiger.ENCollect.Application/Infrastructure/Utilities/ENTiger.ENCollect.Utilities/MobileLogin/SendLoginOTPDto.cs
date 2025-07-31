namespace ENTiger.ENCollect
{
    public class SendLoginOTPDto : DtoBridge
    {
        public string Otp { get; set; }
        public string ExpiryTime { get; set; }
        public string Signature { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}