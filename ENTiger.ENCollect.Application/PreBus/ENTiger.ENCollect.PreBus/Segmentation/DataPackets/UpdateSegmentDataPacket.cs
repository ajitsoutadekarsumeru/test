using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateSegmentDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateSegmentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateSegmentDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateSegmentDataPacket(ILogger<UpdateSegmentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}