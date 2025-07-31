using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ExecuteSegmentDataPacket : FlexiFlowDataPacketWithDtoBridge<ExecuteSegmentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ExecuteSegmentDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ExecuteSegmentDataPacket(ILogger<ExecuteSegmentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}