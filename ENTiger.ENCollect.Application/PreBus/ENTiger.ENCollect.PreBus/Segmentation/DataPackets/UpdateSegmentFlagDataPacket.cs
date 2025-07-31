using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SegmentationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateSegmentFlagDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateSegmentFlagDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateSegmentFlagDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateSegmentFlagDataPacket(ILogger<UpdateSegmentFlagDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}