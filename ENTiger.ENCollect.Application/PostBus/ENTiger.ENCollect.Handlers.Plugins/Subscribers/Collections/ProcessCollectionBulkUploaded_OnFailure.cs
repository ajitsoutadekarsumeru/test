using System.Threading.Tasks;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class ProcessCollectionBulkUploaded
    {
        const string CONDITION_ONFAILURE = "OnFailure";

        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {

            CollectionBulkUploadFailedEvent @event = new CollectionBulkUploadFailedEvent
                {
                    AppContext = _flexAppContext,               
                    Id = _model.Id
               
            };
                    
            await serviceBusContext.Publish(@event);
        }
    }
}