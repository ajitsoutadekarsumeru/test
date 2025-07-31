namespace ENTiger.ENCollect.FeedbackModule
{
    public partial class ProcessBulkTrailUploaded
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            BulkTrailProcessedEvent @event = new BulkTrailProcessedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}