namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessMastersImportUploaded
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            MastersImportProcessedEvent @event = new MastersImportProcessedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = inputmodel.Id,
                Status = filestatus,

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}