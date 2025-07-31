namespace ENTiger.ENCollect.AgencyUsersModule
{
    public class AgentRejected : FlexEventBridge<FlexAppContextBridge>
    {
        public List<string> Ids { get; set; }
    }
}