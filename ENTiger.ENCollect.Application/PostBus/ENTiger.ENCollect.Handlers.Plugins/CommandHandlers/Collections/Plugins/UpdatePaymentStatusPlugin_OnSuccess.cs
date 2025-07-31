using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class UpdatePaymentStatusPlugin : FlexiPluginBase, IFlexiPlugin<UpdatePaymentStatusPostBusDataPacket>
    {
        protected const string CONDITION_ONSUCCESS = "OnSuccess";

        /// <summary>
        /// Publishes the <see cref="OnlineCollectionAddedEvent"/> to the service bus 
        /// once payment status update is successful.
        /// </summary>
        /// <param name="serviceBusContext">The service bus context used to publish the event.</param>
        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            var @event = new UpdatePaymentStatusEvent
            {
                AppContext = _flexAppContext, // Required: sets the context for the event
                // Add more event properties here if needed
                //Ids = _collectionIds
            };

            await serviceBusContext.Publish(@event);
        }
    }
}
