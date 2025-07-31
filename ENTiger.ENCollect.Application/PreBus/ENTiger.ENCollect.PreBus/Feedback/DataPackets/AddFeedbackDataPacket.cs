using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddFeedbackDataPacket : FlexiFlowDataPacketWithDtoBridge<AddFeedbackDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddFeedbackDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddFeedbackDataPacket(ILogger<AddFeedbackDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}