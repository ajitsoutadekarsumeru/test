using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateSegmentsSequenceDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateSegmentsSequenceDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateSegmentsSequenceDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateSegmentsSequenceDataPacket(ILogger<UpdateSegmentsSequenceDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}