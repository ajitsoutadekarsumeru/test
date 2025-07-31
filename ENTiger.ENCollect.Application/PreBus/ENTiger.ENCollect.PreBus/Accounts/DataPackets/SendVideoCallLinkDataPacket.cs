using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SendVideoCallLinkDataPacket : FlexiFlowDataPacketWithDtoBridge<SendVideoCallLinkDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SendVideoCallLinkDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SendVideoCallLinkDataPacket(ILogger<SendVideoCallLinkDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}