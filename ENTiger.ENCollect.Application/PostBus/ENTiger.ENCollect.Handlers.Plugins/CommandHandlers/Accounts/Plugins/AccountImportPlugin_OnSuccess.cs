using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AccountImportPlugin : FlexiPluginBase, IFlexiPlugin<AccountImportPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            AccountImportUploadedEvent @event = new AccountImportUploadedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id
            };
            await serviceBusContext.Publish(@event);
        }
    }
}