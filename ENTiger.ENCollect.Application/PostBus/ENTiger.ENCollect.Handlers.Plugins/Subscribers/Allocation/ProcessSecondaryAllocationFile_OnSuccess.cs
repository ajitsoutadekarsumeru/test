namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessSecondaryAllocationFile
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            SecondaryAllocationProcessedEvent @event = new SecondaryAllocationProcessedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _fileId,
                FileType = _allocationType
            };
            await serviceBusContext.Publish(@event);
        }
    }
}