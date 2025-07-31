
namespace ENTiger.ENCollect.AccountsModule
{
    public partial class SendCustomerConsentMessageDto : DtoBridge
    {
        public string AccountId { get; set; }
        public string Template { get; set; }
        public string? ContactNumber { get; set; }
    }
}
