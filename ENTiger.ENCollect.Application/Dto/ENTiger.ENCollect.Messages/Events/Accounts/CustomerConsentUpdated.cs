
namespace ENTiger.ENCollect.AccountsModule
{
    public class CustomerConsentUpdated : FlexEventBridge<FlexAppContextBridge>
    {
        public string ConsentId { get; set; }
        public string AccountId { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }
    }

    
}
