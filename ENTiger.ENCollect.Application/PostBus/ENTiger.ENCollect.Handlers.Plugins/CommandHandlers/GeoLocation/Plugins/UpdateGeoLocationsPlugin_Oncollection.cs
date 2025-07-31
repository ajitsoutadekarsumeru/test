using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.GeoLocationModule
{
    public partial class UpdateGeoLocationsPlugin : FlexiPluginBase, IFlexiPlugin<UpdateGeoLocationsPostBusDataPacket>
    {
        const string CONDITION_ONCOLLECTION = "Oncollection";

        protected virtual async Task Oncollection(IFlexServiceBusContextBridge serviceBusContext)
        {

            CollectionGeoLocationUpdateRequested @event = new CollectionGeoLocationUpdateRequested
                {
                    AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
                CollectionId = _collectionId,
                GeoLocation = _collectionLocation
            };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}