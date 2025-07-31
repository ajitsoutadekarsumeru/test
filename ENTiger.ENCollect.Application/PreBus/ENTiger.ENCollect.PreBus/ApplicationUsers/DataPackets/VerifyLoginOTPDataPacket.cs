using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class VerifyLoginOTPDataPacket : FlexiFlowDataPacketWithDtoBridge<VerifyLoginOTPDto, FlexAppContextBridge>
    {
        protected readonly ILogger<VerifyLoginOTPDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public VerifyLoginOTPDataPacket(ILogger<VerifyLoginOTPDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public TokenDto OutputDto { get; set; }

        #endregion "Properties
    }
}