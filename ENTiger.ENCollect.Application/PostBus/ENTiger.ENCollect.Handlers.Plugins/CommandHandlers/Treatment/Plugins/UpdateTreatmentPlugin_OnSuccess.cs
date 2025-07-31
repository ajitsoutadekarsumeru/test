using Sumeru.Flex;

namespace ENTiger.ENCollect.TreatmentModule
{
    public partial class UpdateTreatmentPlugin : FlexiPluginBase, IFlexiPlugin<UpdateTreatmentPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            TreatmentUpdatedEvent @event = new TreatmentUpdatedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
            };

            await serviceBusContext.Publish(@event);
        }
    }
}