namespace ENTiger.ENCollect.AccountsModule
{
    public partial class ProcessAccountImportUploaded
    {
        protected const string CONDITION_ONFAILURE = "OnFailure";

        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {
            AccountImportFailedEvent @event = new AccountImportFailedEvent
            {
                AppContext = _flexAppContext,
                Id = _model.Id,
                Remarks = message,
                FileName = errorFileName
            };
            await serviceBusContext.Publish(@event);
        }
    }
}