using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class AckPayInSlipPlugin : FlexiPluginBase, IFlexiPlugin<AckPayInSlipPostBusDataPacket>
    {
        const string CONDITION_ForProjection = "ForProjection";

        protected virtual async Task ForProjection(IFlexServiceBusContextBridge serviceBusContext)
        {

            UpdateCollectionStatusOnPayInSlipAcknowledgedEvent @event = new UpdateCollectionStatusOnPayInSlipAcknowledgedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
               
                PayInSlipId = payinslipId
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);

        }
    }
}