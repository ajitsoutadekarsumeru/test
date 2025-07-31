using ENTiger.ENCollect.AuditTrailModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    public partial class AckPayInSlipPlugin : FlexiPluginBase, IFlexiPlugin<AckPayInSlipPostBusDataPacket>
    {
        protected const string CONDITION_ONAUDITREQUEST = "OnAuditRequest";

        protected virtual async Task OnAuditRequest(IFlexServiceBusContextBridge serviceBusContext)
        {
            AddAuditTrailCommand addAuditTrailCommand = new AddAuditTrailCommand
            {
                Data = _auditData
            };

            await serviceBusContext.Send(addAuditTrailCommand);
        }
    }
}
