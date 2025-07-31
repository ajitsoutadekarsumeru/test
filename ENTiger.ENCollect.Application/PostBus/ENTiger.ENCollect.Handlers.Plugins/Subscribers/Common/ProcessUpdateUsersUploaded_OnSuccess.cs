namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessUpdateUsersUploaded
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            UpdateUsersProcessed @event = new UpdateUsersProcessed
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = model.Id
                //Add your properties here
            };
            await serviceBusContext.Publish(@event);
        }
    }
}