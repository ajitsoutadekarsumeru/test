namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessUpdateUsersUploaded
    {
        protected const string CONDITION_ONFAILURE = "OnFailure";

        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {
            UpdateUsersFailed @event = new UpdateUsersFailed
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = model.Id
                //Add your properties here
            };
            await serviceBusContext.Publish(@event);
        }
    }
}