namespace ENTiger.ENCollect
{
    public interface ILoanAccountContactHistoryQueryRepository
    {
        Task<bool> GetAccountContactHistoryExistsAsync(ContactSourceEnum source, ContactTypeEnum contactType, string contactValue, string accountId, FlexAppContextBridge context);
    }
}
