using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class AckPayInSlipPlugin : FlexiPluginBase, IFlexiPlugin<AckPayInSlipPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            PayInSlipAcknowledgedEvent @event = new PayInSlipAcknowledgedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                PayInSlipIds = payInSlipIds,
                PayInSlipId = payinslipId
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);

        }
    }
}