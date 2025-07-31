using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SendOTPToVerifyNumberDataPacket : FlexiFlowDataPacketWithDtoBridge<SendOTPToVerifyNumberDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SendOTPToVerifyNumberDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SendOTPToVerifyNumberDataPacket(ILogger<SendOTPToVerifyNumberDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        public SendOTPToVerifyNumberResultModel output { get; set; }

        #endregion "Properties
    }
}