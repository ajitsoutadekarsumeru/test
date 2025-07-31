namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCreateUsersUploaded
    {
        private const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            CreateUsersProcessed @event = new CreateUsersProcessed
            {
                AppContext = _flexAppContext,  //do not remove this line

                Id = model.Id,
                FilePath = model.FilePath,
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}