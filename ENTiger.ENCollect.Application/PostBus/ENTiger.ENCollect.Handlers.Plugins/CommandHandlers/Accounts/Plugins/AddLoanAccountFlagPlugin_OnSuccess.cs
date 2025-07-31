using Sumeru.Flex;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class AddLoanAccountFlagPlugin : FlexiPluginBase, IFlexiPlugin<AddLoanAccountFlagPostBusDataPacket>
    {
        private const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            FlagAdded @event = new FlagAdded
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}