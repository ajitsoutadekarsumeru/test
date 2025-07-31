using ENTiger.ENCollect.AuditTrailModule;
using Sumeru.Flex;

namespace ENTiger.ENCollect.ApplicationUsersModule.ADLoginApplicationUsersPlugins
{
    public partial class ADLogin : FlexiBusinessRuleBase, IFlexiBusinessRule<ADLoginDataPacket>
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
