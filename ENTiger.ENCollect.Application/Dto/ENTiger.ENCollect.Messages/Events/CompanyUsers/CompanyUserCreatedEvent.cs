namespace ENTiger.ENCollect.CompanyUsersModule
{
    public class CompanyUserCreatedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string? Id { get; set; }
    }
}