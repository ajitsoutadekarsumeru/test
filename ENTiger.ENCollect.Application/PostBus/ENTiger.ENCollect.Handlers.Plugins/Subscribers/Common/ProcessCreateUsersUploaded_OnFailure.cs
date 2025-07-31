namespace ENTiger.ENCollect.CommonModule
{
    public partial class ProcessCreateUsersUploaded
    {
        private const string CONDITION_ONFAILURE = "OnFailure";

        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {
            CreateUsersFailed @event = new CreateUsersFailed
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = model.Id
                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}