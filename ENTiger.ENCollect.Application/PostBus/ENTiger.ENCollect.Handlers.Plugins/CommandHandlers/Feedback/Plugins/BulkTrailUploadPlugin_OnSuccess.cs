using Sumeru.Flex;

namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class BulkTrailUploadPlugin : FlexiPluginBase, IFlexiPlugin<BulkTrailUploadPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            BulkTrailUploadedEvent @event = new BulkTrailUploadedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id,

                CustomId = _model.CustomId,
                FileName = _model.FileName

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}