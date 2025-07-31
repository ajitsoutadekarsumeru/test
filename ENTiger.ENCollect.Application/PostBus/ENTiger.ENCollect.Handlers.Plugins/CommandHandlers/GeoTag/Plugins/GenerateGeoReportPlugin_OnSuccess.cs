using ENTiger.ENCollect.PermissionSchemesModule;
using Sumeru.Flex;
using System;

namespace ENTiger.ENCollect.GeoTagModule
{
    public partial class GenerateGeoReportPlugin : FlexiPluginBase, IFlexiPlugin<GenerateGeoReportPostBusDataPacket>
    {
        private const string CONDITION_ONSUCCESS = "OnSuccess";

        /// <summary>
        /// Publishes the <see cref="GeoReportGenerated"/> event upon successful report generation.
        /// </summary>
        /// <param name="serviceBusContext">Service bus context used for event publishing.</param>
        protected virtual async Task OnSuccess(IFlexServiceBusContextBridge serviceBusContext)
        {
            var @event = new GeoReportGenerated
            {
                AppContext = _flexAppContext!,  // Required context. Do not remove.
                FileName = fileName,
            };

            await serviceBusContext.Publish(@event);
        }
    }
}
