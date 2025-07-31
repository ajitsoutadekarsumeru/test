
namespace ENTiger.ENCollect.AccountsModule
{
    public partial class RequestCustomerConsentDto : DtoBridge
    {
        public string AccountId { get; set; }
        public DateTime? RequestedVisitTime { get; set; }
    }


}
