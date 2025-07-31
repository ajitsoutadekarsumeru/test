using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.GeoLocationModule
{
    public class UserGeoLocationUpdateRequested : FlexEventBridge<FlexAppContextBridge>
    {
        public string UserAttendanceId { get; set; } 
        public string GeoLocation { get; set; }
    }    
}
