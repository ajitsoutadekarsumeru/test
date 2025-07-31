using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DisableSegmentsDataPacket : FlexiFlowDataPacketWithDtoBridge<DisableSegmentsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<DisableSegmentsDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public DisableSegmentsDataPacket(ILogger<DisableSegmentsDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}