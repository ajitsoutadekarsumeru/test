using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.GeoLocationModule
{
    public class FeedbackGeoLocationUpdateRequested : FlexEventBridge<FlexAppContextBridge>
    {
        public string FeedbackId { get; set; }
        public string GeoLocation { get; set; }
    }    
}
