namespace ENTiger.ENCollect.Messages.Events.License
{
    public class UserLicenseLimitReachedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string UserType { get; set; }
        public string UserId { get; set; }
    }
}
