using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class VerifyAddNumberOTPDataPacket : FlexiFlowDataPacketWithDtoBridge<VerifyAddNumberOTPDto, FlexAppContextBridge>
    {
        protected readonly ILogger<VerifyAddNumberOTPDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public VerifyAddNumberOTPDataPacket(ILogger<VerifyAddNumberOTPDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public VerifyAddNumberOTPResultModel output { get; set; }

        #endregion "Properties
    }
}