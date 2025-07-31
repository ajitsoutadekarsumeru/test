using ENTiger.ENCollect.SettlementModule;

namespace ENTiger.ENCollect
{
    public class SettlementRequestorService
    {
        private readonly ISettlementRepository _repoSettlement;

        public SettlementRequestorService(
            ISettlementRepository repoSettlement)
        {
            _repoSettlement = repoSettlement;
        }

        public async Task<int> GetRequestorLevel(FlexAppContextBridge context, string requestorId)
        {
            int level = 0;
            var settlement = await _repoSettlement.GetByIdAsync(context, requestorId);
            return level;
        }
    }
}
