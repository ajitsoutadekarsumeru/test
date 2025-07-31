using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class MastersImportPlugin : FlexiPluginBase, IFlexiPlugin<MastersImportPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            MastersImportUploadedEvent @event = new MastersImportUploadedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}