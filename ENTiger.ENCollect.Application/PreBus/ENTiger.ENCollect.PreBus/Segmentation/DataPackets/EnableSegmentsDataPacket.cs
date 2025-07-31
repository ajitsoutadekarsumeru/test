using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class EnableSegmentsDataPacket : FlexiFlowDataPacketWithDtoBridge<EnableSegmentsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<EnableSegmentsDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public EnableSegmentsDataPacket(ILogger<EnableSegmentsDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}