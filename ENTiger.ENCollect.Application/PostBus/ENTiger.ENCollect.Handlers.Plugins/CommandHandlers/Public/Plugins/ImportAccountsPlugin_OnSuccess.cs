using Sumeru.Flex;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class ImportAccountsPlugin : FlexiPluginBase, IFlexiPlugin<ImportAccountsPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            ImportAccountsUploadedEvent @event = new ImportAccountsUploadedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
                Id = _model.Id,
                CustomId = _model.CustomId
            };

            await serviceBusContext.Publish(@event);
        }
    }
}