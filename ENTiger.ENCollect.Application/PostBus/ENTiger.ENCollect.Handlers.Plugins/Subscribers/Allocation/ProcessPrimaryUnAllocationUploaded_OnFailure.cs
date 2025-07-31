namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessPrimaryUnAllocationUploaded
    {
        protected const string CONDITION_ONFAILURE = "OnFailure";

        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {
            PrimaryUnAllocationFailedEvent @event = new PrimaryUnAllocationFailedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                CustomId = _model?.CustomId,
                Email = _user?.PrimaryEMail,
                UnAllocationType= _unAllocationType
            };                    
            await serviceBusContext.Publish(@event);
        }
    }
}