namespace ENTiger.ENCollect.CompanyUsersModule
{
    public class CompanyUserDormant : FlexEventBridge<FlexAppContextBridge>
    {
        public List<string> Ids { get; set; }
    }
}