
namespace ENTiger.ENCollect.AgencyUsersModule
{
    public class AgencyUsersActivated : FlexEventBridge<FlexAppContextBridge>
    {
        public List<string> Ids { get; set; }
    }
}
