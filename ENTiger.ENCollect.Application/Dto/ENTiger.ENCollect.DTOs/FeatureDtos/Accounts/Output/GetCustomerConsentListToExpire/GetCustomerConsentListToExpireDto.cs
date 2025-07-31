namespace ENTiger.ENCollect.AccountsModule
{
    public partial class GetCustomerConsentListToExpireDto :  DtoBridge
    {
        public string? AccountId { get; set; }
        public string? UserId { get; set; }
        public DateTime? RequestedVisitTime { get; set; } 
        public DateTime? ConsentResponseTime { get; set; }  
        public DateTime? ExpiryTime { get; set; }   
        public bool? IsActive { get; set; } 
        public string Status { get; set; }
        public string SecureToken { get; set; }  
    }
}
