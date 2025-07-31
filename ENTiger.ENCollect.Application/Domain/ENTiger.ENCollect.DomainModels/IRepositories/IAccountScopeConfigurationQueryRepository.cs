using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public interface IAccountScopeConfigurationQueryRepository
    {
        Task<List<AccountScopeConfiguration>> GetScopeConfigurations(List<Accountability> accountabilities, FlexAppContextBridge context);
    }
}

