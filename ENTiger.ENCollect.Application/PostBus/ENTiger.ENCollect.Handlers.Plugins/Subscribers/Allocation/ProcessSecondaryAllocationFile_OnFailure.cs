namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessSecondaryAllocationFile
    {
        protected const string CONDITION_ONFAILURE = "OnFailure";

        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {
            SecondaryAllocationFailedEvent @event = new SecondaryAllocationFailedEvent
            {
                AppContext = _flexAppContext,  //do not remove this
                Id = _fileId,
                FileType = _allocationType
            };
            await serviceBusContext.Publish(@event);
        }
    }
}