using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AddLoanAccountFlagPlugin : FlexiPluginBase, IFlexiPlugin<AddLoanAccountFlagPostBusDataPacket>
    {
        private const string CONDITION_ONFAILURE = "OnFailure";

        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {
            FlagAdditionFailed @event = new FlagAdditionFailed
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}