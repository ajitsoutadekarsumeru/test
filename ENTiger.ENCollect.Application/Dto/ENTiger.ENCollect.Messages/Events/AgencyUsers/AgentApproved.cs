namespace ENTiger.ENCollect.AgencyUsersModule
{
    public class AgentApproved : FlexEventBridge<FlexAppContextBridge>
    {
        public List<string> Ids { get; set; }
    }
}