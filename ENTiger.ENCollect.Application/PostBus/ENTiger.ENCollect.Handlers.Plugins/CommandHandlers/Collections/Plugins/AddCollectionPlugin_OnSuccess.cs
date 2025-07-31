using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class AddCollectionPlugin : FlexiPluginBase, IFlexiPlugin<AddCollectionPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            CollectionAddedEvent @event = new CollectionAddedEvent
            {
                AppContext = _flexAppContext,  //do not remove this line
                Id = _model.Id,
                CollectorId = _model.CollectorId,
                ReservationId = _reservationId
            };
            await serviceBusContext.Publish(@event);
        }
    }
}