using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddSegmentDataPacket : FlexiFlowDataPacketWithDtoBridge<AddSegmentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddSegmentDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddSegmentDataPacket(ILogger<AddSegmentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}