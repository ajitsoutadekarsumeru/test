namespace ENTiger.ENCollect
{
    public interface IAccountFetchStrategy
    {
        string SupportedTriggerTypeId { get; }
        string SupportedTriggerType { get; }
        Task<IReadOnlyList<string>> IdentifyActorsAsync(CommunicationTrigger trigger, FlexAppContextBridge context);
    }
}