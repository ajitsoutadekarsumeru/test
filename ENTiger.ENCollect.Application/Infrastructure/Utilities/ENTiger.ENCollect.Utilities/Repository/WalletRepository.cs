using ENTiger.ENCollect.DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class WalletRepository : IWalletRepository
    {
        private readonly IRepoFactory _repoFactory;
        private readonly WalletSettings _walletSettings;

        public WalletRepository(IRepoFactory repoFactory, IOptions<WalletSettings> walletSettings)
        {
            _repoFactory = repoFactory;
            _walletSettings = walletSettings.Value;
        }

        public async Task<Wallet?> GetByAgentIdAsync(FlexAppContextBridge context, string agentId)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                            .FindAll<Wallet>()
                            .Where(w => w.AgentId == agentId)
                            .FlexInclude(w => w.Reservations)
                            .FirstOrDefaultAsync();
        }
                

        public async Task<string> GetUtilizationColor(decimal? utilizedPercentage)
        {
           
            if (utilizedPercentage < _walletSettings.GreenThreshold)
                return RAGColorEnum.Green.Value;

            if (utilizedPercentage < _walletSettings.AmberThreshold)
                return RAGColorEnum.Amber.Value;

            return RAGColorEnum.Red.Value;
        }

        public async Task SaveAsync(FlexAppContextBridge context, Wallet wallet)
        {
            _repoFactory.Init(context);
            _repoFactory.GetRepo().InsertOrUpdate(wallet);
            await _repoFactory.GetRepo().SaveAsync();
        }
        
    }
}
