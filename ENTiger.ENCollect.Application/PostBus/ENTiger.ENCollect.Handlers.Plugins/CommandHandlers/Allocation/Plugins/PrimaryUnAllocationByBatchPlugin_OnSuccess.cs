using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class PrimaryUnAllocationByBatchPlugin : FlexiPluginBase, IFlexiPlugin<PrimaryUnAllocationByBatchPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            PrimaryUnAllocationUploadedEvent @event = new PrimaryUnAllocationUploadedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id,
                UnAllocationType= _unAllocationType

            };                    
            await serviceBusContext.Publish(@event);
        }
    }
}