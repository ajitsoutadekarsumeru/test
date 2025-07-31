namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class ProcessBulkTrailUploaded
    {
        protected const string CONDITION_ONFAILURE = "OnFailure";

        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {
            BulkTrailFailedEvent @event = new BulkTrailFailedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}