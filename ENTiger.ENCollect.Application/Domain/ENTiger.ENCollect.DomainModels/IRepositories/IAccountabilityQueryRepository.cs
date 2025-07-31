using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public interface IAccountabilityQueryRepository
    {
        Task<List<Accountability>> GetAccountabilities(string userId, FlexAppContextBridge context);
    }
}
