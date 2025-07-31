namespace ENTiger.ENCollect.AgencyUsersModule
{
    public class AgentDormant : FlexEventBridge<FlexAppContextBridge>
    {
        public List<string> Ids { get; set; }
    }
}