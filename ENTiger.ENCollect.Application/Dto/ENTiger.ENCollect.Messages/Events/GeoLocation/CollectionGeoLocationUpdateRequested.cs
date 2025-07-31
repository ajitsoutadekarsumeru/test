using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.GeoLocationModule
{
    public class CollectionGeoLocationUpdateRequested : FlexEventBridge<FlexAppContextBridge>
    {
        public string CollectionId { get; set; }
        public string GeoLocation { get; set; }
    }

    
}
