namespace ENTiger.ENCollect.AccountsModule
{
    public partial class SendVideoCallLinkDto : DtoBridge
    {
        public string AccountId { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string Link { get; set; }
        public string CustomerName { get; set; }
    }
}