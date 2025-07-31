using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SendPaymentCopyViaEmailDataPacket : FlexiFlowDataPacketWithDtoBridge<SendPaymentCopyViaEmailDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SendPaymentCopyViaEmailDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SendPaymentCopyViaEmailDataPacket(ILogger<SendPaymentCopyViaEmailDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}