using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetFeedbackImageDataPacket : FlexiFlowDataPacketWithDtoBridge<GetFeedbackImageDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetFeedbackImageDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetFeedbackImageDataPacket(ILogger<GetFeedbackImageDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}