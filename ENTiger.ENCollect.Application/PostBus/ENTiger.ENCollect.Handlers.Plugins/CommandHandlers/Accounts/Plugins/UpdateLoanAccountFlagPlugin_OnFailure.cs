using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateLoanAccountFlagPlugin : FlexiPluginBase, IFlexiPlugin<UpdateLoanAccountFlagPostBusDataPacket>
    {
        private const string CONDITION_ONFAILURE = "OnFailure";

        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {
            FlagUpdationFailed @event = new FlagUpdationFailed
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}