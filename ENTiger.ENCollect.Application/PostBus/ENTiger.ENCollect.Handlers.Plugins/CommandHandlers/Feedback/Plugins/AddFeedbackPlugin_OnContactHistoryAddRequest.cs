using ENTiger.ENCollect.Messages.Commands.ContactHistory;
using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class AddFeedbackPlugin : FlexiPluginBase, IFlexiPlugin<AddFeedbackPostBusDataPacket>
    {
        protected const string CONDITION_OnContactHistoryAddRequest = "OnContactHistoryAddRequest";

        protected virtual async Task OnContactHistoryAddRequest(IFlexServiceBusContextBridge serviceBusContext)
        {
            AddContactHistoryCommand addContactHistoryCommand = new AddContactHistoryCommand
            {
                flexAppContext=_flexAppContext,
                data = _contactHistoryData
            };

            await serviceBusContext.Send(addContactHistoryCommand);
        }
    }
}
