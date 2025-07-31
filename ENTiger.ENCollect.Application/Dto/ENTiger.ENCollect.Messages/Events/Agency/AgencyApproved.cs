namespace ENTiger.ENCollect.AgencyModule
{
    public class AgencyApproved : FlexEventBridge<FlexAppContextBridge>
    {
        public List<string> Ids { get; set; }
    }
}