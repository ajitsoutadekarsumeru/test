namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public class TransactionLicenseLimitExceeded : FlexEventBridge<FlexAppContextBridge>
    {
        public string TransactionType { get; set; }
        public string UserId { get; set; }
    }
}
