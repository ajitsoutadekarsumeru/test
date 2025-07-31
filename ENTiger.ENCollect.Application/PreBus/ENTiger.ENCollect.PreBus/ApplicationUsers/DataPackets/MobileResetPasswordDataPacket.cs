using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class MobileResetPasswordDataPacket : FlexiFlowDataPacketWithDtoBridge<MobileResetPasswordDto, FlexAppContextBridge>
    {
        protected readonly ILogger<MobileResetPasswordDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public MobileResetPasswordDataPacket(ILogger<MobileResetPasswordDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string? Key { get; set; }

        #endregion "Properties
    }
}