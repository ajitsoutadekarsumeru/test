using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SendAccountMessageDataPacket : FlexiFlowDataPacketWithDtoBridge<SendAccountMessageDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SendAccountMessageDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SendAccountMessageDataPacket(ILogger<SendAccountMessageDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}