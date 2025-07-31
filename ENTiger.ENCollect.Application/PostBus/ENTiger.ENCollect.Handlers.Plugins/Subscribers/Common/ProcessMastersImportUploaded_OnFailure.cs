namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessMastersImportUploaded
    {
        protected const string CONDITION_ONFAILURE = "OnFailure";

        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {
            MastersImportFailedEvent @event = new MastersImportFailedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = inputmodel.Id
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}