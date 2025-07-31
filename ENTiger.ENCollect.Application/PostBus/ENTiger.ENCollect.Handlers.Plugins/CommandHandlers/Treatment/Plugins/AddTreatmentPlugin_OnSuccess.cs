using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class AddTreatmentPlugin : FlexiPluginBase, IFlexiPlugin<AddTreatmentPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            TreatmentAddedEvent @event = new TreatmentAddedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}