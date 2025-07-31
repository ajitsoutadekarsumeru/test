using ENTiger.ENCollect.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public interface IWalletRepository
    {
        // Retrieves the Wallet aggregate for a given agent.
        Task<Wallet?> GetByAgentIdAsync(FlexAppContextBridge context, string agentId);
        Task<string> GetUtilizationColor(decimal? utilizedPercentage);

        // Persists changes to the Wallet aggregate.
        Task SaveAsync(FlexAppContextBridge context, Wallet wallet);
       
    }
}
