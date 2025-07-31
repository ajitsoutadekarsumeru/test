using Sumeru.Flex;

namespace ENTiger.ENCollect.CommonModule
{
    public partial class CreateUsersByBatchPlugin : FlexiPluginBase, IFlexiPlugin<CreateUsersByBatchPostBusDataPacket>
    {
        private const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            CreateUsersUploadedEvent @event = new CreateUsersUploadedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id
            };
            await serviceBusContext.Publish(@event);
        }
    }
}