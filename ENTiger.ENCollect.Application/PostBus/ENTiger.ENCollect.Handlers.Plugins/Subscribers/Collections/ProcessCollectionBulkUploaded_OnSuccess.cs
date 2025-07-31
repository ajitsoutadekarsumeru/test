using System.Threading.Tasks;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionBulkUploaded
    {
        const string CONDITION_ONSUCCESS = "OnSuccess";

        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {

            CollectionBulkUploadProcessedEvent @event = new CollectionBulkUploadProcessedEvent
                {
                    AppContext = _flexAppContext, 
                    Id = _model.Id
                
            };
                    
            await serviceBusContext.Publish(@event);
        }
    }
}