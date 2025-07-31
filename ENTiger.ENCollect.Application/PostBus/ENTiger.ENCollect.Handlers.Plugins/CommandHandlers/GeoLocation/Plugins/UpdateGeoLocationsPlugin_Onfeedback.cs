using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.GeoLocationModule
{
    public partial class UpdateGeoLocationsPlugin : FlexiPluginBase, IFlexiPlugin<UpdateGeoLocationsPostBusDataPacket>
    {
        const string CONDITION_ONFEEDBACK = "Onfeedback";

        protected virtual async Task Onfeedback(IFlexServiceBusContextBridge serviceBusContext)
        {

            FeedbackGeoLocationUpdateRequested @event = new FeedbackGeoLocationUpdateRequested
            {
                AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
                FeedbackId = _feedbackId,
                GeoLocation = _feedbackLocation
                };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}