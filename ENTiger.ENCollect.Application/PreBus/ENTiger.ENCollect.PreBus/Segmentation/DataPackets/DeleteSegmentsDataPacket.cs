using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeleteSegmentsDataPacket : FlexiFlowDataPacketWithDtoBridge<DeleteSegmentsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<DeleteSegmentsDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public DeleteSegmentsDataPacket(ILogger<DeleteSegmentsDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}