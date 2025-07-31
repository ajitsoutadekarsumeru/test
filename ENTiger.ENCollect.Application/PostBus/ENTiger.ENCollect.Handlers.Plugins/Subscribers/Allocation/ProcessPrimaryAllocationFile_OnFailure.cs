namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessPrimaryAllocationFile
    {
        protected const string CONDITION_ONFAILURE = "OnFailure";

        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {
            PrimaryAllocationFailedEvent @event = new PrimaryAllocationFailedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _fileId,
                FileType = _allocationType
            };
            await serviceBusContext.Publish(@event);
        }
    }
}