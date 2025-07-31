using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SendPaymentLinkDataPacket : FlexiFlowDataPacketWithDtoBridge<SendPaymentLinkDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SendPaymentLinkDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SendPaymentLinkDataPacket(ILogger<SendPaymentLinkDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string ReceiptNo { get; set; }

        #endregion "Properties
    }
}