namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessSecondaryUnAllocationUploaded
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            SecondaryUnAllocationProcessedEvent @event = new SecondaryUnAllocationProcessedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                CustomId = _model.CustomId,
                EMail = _user?.PrimaryEMail,
                FileName = _model.FileName,
                UnAllocationType= _unAllocationType
            };                    
            await serviceBusContext.Publish(@event);
        }
    }
}