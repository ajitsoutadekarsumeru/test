using Sumeru.Flex;

namespace ENTiger.ENCollect.AllocationModule
{
    public partial class SecondaryAllocationByFilterPlugin : FlexiPluginBase, IFlexiPlugin<SecondaryAllocationByFilterPostBusDataPacket>
    {
        protected const string CONDITION_ONFAILURE = "OnFailure";

        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {
            PrimaryAllocationByFilterFailedEvent @event = new PrimaryAllocationByFilterFailedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}