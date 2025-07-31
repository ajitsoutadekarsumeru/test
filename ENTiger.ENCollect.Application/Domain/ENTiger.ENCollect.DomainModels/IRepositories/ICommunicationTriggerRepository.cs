using ENTiger.ENCollect.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public interface ICommunicationTriggerRepository
    {
        Task<IEnumerable<CommunicationTrigger>> GetAllActiveAsync(FlexAppContextBridge context);
        Task EnqueueRangeAsync(FlexAppContextBridge context,string runId, string triggerId, IReadOnlyList<string> accountIds);
        Task<List<TriggerDeliverySpec>> GetDeliverySpecsAsync(string triggerId, FlexAppContextBridge flexAppContext);
    }
}
