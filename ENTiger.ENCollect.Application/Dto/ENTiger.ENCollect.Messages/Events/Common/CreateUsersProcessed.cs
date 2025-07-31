namespace ENTiger.ENCollect.CommonModule
{
    public class CreateUsersProcessed : FlexEventBridge<FlexAppContextBridge>
    {
        public string? Id { get; set; }

        public string? FilePath { get; set; }
    }
}