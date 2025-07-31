namespace ENTiger.ENCollect.CompanyUsersModule
{
    public class CompanyUserApproved : FlexEventBridge<FlexAppContextBridge>
    {
        public List<string>? Ids { get; set; }
    }
}