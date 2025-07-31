namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountImportUploaded
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            AccountImportProcessedEvent @event = new AccountImportProcessedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id
            };
            await serviceBusContext.Publish(@event);
        }
    }
}