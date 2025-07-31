using Sumeru.Flex;

namespace ENTiger.ENCollect.AuditTrailModule
{
    public partial class AddAuditTrailPlugin : FlexiPluginBase, IFlexiPlugin<AddAuditTrailPostBusDataPacket>
    {
        const string CONDITION_ONAUDITTRAIL = "OnAuditTrail";

        protected virtual async Task OnAuditTrail(IFlexServiceBusContextBridge serviceBusContext)
        {

            AuditTrailRequestedEvent @event = new AuditTrailRequestedEvent
            {
                AppContext = _flexAppContext
            };
                    
            await serviceBusContext.Publish(@event);
        }
    }
}