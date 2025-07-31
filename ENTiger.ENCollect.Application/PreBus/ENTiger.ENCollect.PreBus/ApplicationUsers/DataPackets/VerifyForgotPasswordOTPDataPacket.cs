using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class VerifyForgotPasswordOTPDataPacket : FlexiFlowDataPacketWithDtoBridge<VerifyForgotPasswordOTPDto, FlexAppContextBridge>
    {
        protected readonly ILogger<VerifyForgotPasswordOTPDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public VerifyForgotPasswordOTPDataPacket(ILogger<VerifyForgotPasswordOTPDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string OutputDto { get; set; }

        #endregion "Properties
    }
}