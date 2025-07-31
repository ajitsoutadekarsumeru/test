namespace ENTiger.ENCollect.AllocationModule
{
    public partial class ProcessPrimaryUnAllocationUploaded
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            PrimaryUnAllocationProcessedEvent @event = new PrimaryUnAllocationProcessedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                CustomId = _model.CustomId,
                EMail = _user?.PrimaryEMail,
                FileName = _model.FileName,
                UnAllocationType = _unAllocationType
            };                    
            await serviceBusContext.Publish(@event);
        }
    }
}