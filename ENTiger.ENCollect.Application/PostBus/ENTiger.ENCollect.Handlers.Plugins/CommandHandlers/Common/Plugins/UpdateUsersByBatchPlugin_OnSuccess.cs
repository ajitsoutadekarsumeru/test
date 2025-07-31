using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class UpdateUsersByBatchPlugin : FlexiPluginBase, IFlexiPlugin<UpdateUsersByBatchPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            UpdateUsersUploadedEvent @event = new UpdateUsersUploadedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id
            };
            await serviceBusContext.Publish(@event);
        }
    }
}