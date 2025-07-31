using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public interface ISendEmailForLicenseTransactionLimit : IAmFlexSubscriber<TransactionLicenseLimitExceeded>
    {
    }
}
