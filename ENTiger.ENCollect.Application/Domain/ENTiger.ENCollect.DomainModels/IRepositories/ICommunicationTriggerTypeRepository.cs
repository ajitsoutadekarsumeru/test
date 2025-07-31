using ENTiger.ENCollect.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public interface ITriggerTypeRepository
    {
        // Retrieves the Wallet aggregate for a given agent.
        Task<TriggerType?> GetByTypeIdAsync(FlexAppContextBridge context, string id);
       

        // Persists changes to the Wallet aggregate.
        Task SaveTriggerAsync(FlexAppContextBridge context, CommunicationTrigger trigger);
        Task SaveAsync(FlexAppContextBridge context, TriggerType trigger);

    }
}
