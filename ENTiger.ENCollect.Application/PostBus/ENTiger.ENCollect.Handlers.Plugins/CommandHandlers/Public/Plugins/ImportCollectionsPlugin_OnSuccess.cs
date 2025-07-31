using ENTiger.ENCollect.CollectionsModule;
using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.PublicModule
{
    public partial class ImportCollectionsPlugin : FlexiPluginBase, IFlexiPlugin<ImportCollectionsPostBusDataPacket>
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            CollectionBulkUploadedEvent @event = new CollectionBulkUploadedEvent
            {
                AppContext = _flexAppContext,

               
                Id = _model.Id,
                CustomId = _model.CustomId
            };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}