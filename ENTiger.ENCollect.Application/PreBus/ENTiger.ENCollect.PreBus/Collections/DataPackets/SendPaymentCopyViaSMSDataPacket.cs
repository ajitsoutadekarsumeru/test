using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SendPaymentCopyViaSMSDataPacket : FlexiFlowDataPacketWithDtoBridge<SendPaymentCopyViaSMSDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SendPaymentCopyViaSMSDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SendPaymentCopyViaSMSDataPacket(ILogger<SendPaymentCopyViaSMSDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}