using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ForgotPasswordDataPacket : FlexiFlowDataPacketWithDtoBridge<ForgotPasswordDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ForgotPasswordDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ForgotPasswordDataPacket(ILogger<ForgotPasswordDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string? Key { get; set; }

        #endregion "Properties
    }
}