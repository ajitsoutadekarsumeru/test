using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ResetPasswordDataPacket : FlexiFlowDataPacketWithDtoBridge<ResetPasswordDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ResetPasswordDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ResetPasswordDataPacket(ILogger<ResetPasswordDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string? Key { get; set; }

        #endregion "Properties
    }
}