namespace ENTiger.ENCollect.CompanyUsersModule
{
    public class CompanyUserRejected : FlexEventBridge<FlexAppContextBridge>
    {
        public List<string> Id { get; set; }
    }
}