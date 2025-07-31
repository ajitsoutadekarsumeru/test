using ENTiger.ENCollect.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public interface ICommunicationTemplateRepository
    {
        // Retrieves the Wallet aggregate for a given agent.
        Task<CommunicationTemplate?> GetByIdAsync(FlexAppContextBridge context, string Id);
       

       

    }
}
