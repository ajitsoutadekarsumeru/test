
namespace ENTiger.ENCollect.AccountsModule
{
    public class CustomerConsentRequested : FlexEventBridge<FlexAppContextBridge>
    {
        public string ConsentId { get; set; }
        public string AccountId {  get; set; }
        public DateTime? AppointmentDate { get; set; }
        public string Link { get; set; }
    }

    
}
