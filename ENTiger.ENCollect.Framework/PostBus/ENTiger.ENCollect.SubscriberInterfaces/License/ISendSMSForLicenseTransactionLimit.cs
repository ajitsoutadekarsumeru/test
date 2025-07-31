using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    public interface ISendSMSForLicenseTransactionLimit : IAmFlexSubscriber<TransactionLicenseLimitExceeded>
    {
    }
}
