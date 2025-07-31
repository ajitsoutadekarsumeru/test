using ENTiger.ENCollect.Messages.Commands.ContactHistory;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class SendPaymentLinkPlugin : FlexiPluginBase, IFlexiPlugin<SendPaymentLinkPostBusDataPacket>
    {
        protected const string CONDITION_OnContactHistoryAddRequest = "OnContactHistoryAddRequest";

        protected virtual async Task OnContactHistoryAddRequest(IFlexServiceBusContextBridge serviceBusContext)
        {
            AddContactHistoryCommand addContactHistoryCommand = new AddContactHistoryCommand
            {
                flexAppContext = _flexAppContext,
                data = _contactHistoryData
            };

            await serviceBusContext.Send(addContactHistoryCommand);
        }
    }
}
