using Sumeru.Flex;


namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class GenerateGeoReportPlugin : FlexiPluginBase, IFlexiPlugin<GenerateGeoReportPostBusDataPacket>
    {
        private const string CONDITION_ONFAILURE = "OnFailure";

        /// <summary>
        /// Publishes the <see cref="GeoReportFailed"/> event upon Failure report generation.
        /// </summary>
        /// <param name="serviceBusContext">Service bus context used for event publishing.</param>
        protected virtual async Task OnFailure(IFlexServiceBusContextBridge serviceBusContext)
        {
            var @event = new GeoReportFailed
            {
                AppContext = _flexAppContext!  // Required context. Do not remove.
            };

            await serviceBusContext.Publish(@event);
        }
    }
}

