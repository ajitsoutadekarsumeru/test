namespace ENTiger.ENCollect.AgencyUsersModule
{
    public class AgentAddedEvent : FlexEventBridge<FlexAppContextBridge>
    {
        public string Id { get; set; }
    }
}