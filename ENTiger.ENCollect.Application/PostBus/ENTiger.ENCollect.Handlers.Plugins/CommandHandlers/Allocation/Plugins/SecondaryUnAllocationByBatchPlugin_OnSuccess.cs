using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SecondaryUnAllocationByBatchPlugin : FlexiPluginBase, IFlexiPlugin<SecondaryUnAllocationByBatchPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            SecondaryUnAllocationUploadedEvent @event = new SecondaryUnAllocationUploadedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id,
                UnAllocationType = _unAllocationType

            };                    
            await serviceBusContext.Publish(@event);
        }
    }
}