using Sumeru.Flex;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.GeoLocationModule
{
    public partial class UpdateGeoLocationsPlugin : FlexiPluginBase, IFlexiPlugin<UpdateGeoLocationsPostBusDataPacket>
    {
        const string CONDITION_ONUSER = "Onuser";

        protected virtual async Task Onuser(IFlexServiceBusContextBridge serviceBusContext)
        {

            UserGeoLocationUpdateRequested @event = new UserGeoLocationUpdateRequested
                {
                    AppContext = _flexAppContext,  //do not remove this line

                //Add your properties here
                UserAttendanceId = _userAttendenceId,
                GeoLocation = _userAttendenceLocation
            };
                    
            await serviceBusContext.Publish(@event);

        }
    }
}