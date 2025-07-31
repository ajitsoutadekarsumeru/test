using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class PrimaryAllocationByBatchPlugin : FlexiPluginBase, IFlexiPlugin<PrimaryAllocationByBatchPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            PrimaryAllocationUploadedEvent @event = new PrimaryAllocationUploadedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id,
                FileType = _model.UploadType,
                CustomId = _model.CustomId,
                FileName = _model.FileName,
                AllocationMethod = _model.Description
            };                    
            await serviceBusContext.Publish(@event);
        }
    }
}