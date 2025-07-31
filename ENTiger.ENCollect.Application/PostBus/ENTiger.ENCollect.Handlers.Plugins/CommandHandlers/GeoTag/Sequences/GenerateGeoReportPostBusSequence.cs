using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    /// Defines the post-bus plugin execution sequence for generating geo reports.
    /// This sequence is triggered after the GeoReport command is dispatched.
    /// </summary>
    public sealed class GenerateGeoReportPostBusSequence : FlexiPluginSequenceBase<GenerateGeoReportPostBusDataPacket>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateGeoReportPostBusSequence"/> class.
        /// Adds the required plugins to the sequence pipeline.
        /// </summary>
        public GenerateGeoReportPostBusSequence()
        {
            Add<GenerateGeoReportPlugin>();
        }
    }
}
