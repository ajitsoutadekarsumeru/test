using System;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Represents a domain event that is published when an HTTP call fails.
    /// In production, this would be published via a messaging system (e.g., NServiceBus).
    /// </summary>
    public class DomainFailureEvent
    {
        public string ApiUrl { get; set; }
        public string Payload { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime Timestamp { get; set; }

        public override string ToString() =>
            $"DomainFailureEvent: ApiUrl={ApiUrl}, Payload={Payload}, ErrorMessage={ErrorMessage}, Timestamp={Timestamp}";
    }
}