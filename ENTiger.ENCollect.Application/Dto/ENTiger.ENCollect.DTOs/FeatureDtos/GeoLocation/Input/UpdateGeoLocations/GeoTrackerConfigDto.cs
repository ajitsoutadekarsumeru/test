using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.GeoLocationModule
{
    public partial class GeoTrackerConfigDto : DtoBridge
    {
        public string? GoogleMapApiKey { get; set; }
        public string? DBSGoogleMapApiUrl { get; set; }
        public string? DBSORGIDGoogleMapApi { get; set; }
        public string? XApiKeyGoogleMapApi { get; set; }
        public string? GeoTrackerDetailsCertifcationPath { get; set; }
        public string? GeoCertificateFlag { get; set; }
        public string? GeoTrackerLogFilePath { get; set; }
    }

}
