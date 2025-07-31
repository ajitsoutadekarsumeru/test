using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class CollectionBulkUploadPlugin : FlexiPluginBase, IFlexiPlugin<CollectionBulkUploadPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            CollectionBulkUploadedEvent @event = new CollectionBulkUploadedEvent
                {
                    AppContext = _flexAppContext,  //do not remove this line

                    //Add your properties here
                    Id = _model.Id,
                    CustomId = _model.CustomId
                };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}