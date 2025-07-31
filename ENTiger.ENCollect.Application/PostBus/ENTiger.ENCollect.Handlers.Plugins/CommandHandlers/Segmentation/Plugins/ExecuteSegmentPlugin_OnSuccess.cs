using Sumeru.Flex;

namespace ENTiger.ENCollect.SegmentationModule
{
    public partial class ExecuteSegmentPlugin : FlexiPluginBase, IFlexiPlugin<ExecuteSegmentPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            SegmentExecuted @event = new SegmentExecuted
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}