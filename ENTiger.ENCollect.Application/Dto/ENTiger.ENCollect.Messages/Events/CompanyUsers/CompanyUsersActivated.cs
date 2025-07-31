
namespace ENTiger.ENCollect.CompanyUsersModule
{
    public class CompanyUsersActivated : FlexEventBridge<FlexAppContextBridge>
    {
        public List<string> Ids { get; set; }
    }
}
