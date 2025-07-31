
namespace ENTiger.ENCollect.AccountsModule
{
    public partial class GetCustomerConsentListDto : DtoBridge
    {
        public string? AccountNo { get; set; }
        public string ConsentId { get; set; }
        public string UserId { get; set; }
        public string Status { get; set; }
        public DateTime? RequestedVisitTime { get; set; }  
        public DateTime? ConsentResponseTime { get; set; } 
        public DateTime? ExpiryTime { get; set; }   
        public DateTime? CreatedDateTime { get; set; } //link sent 
    }
}
