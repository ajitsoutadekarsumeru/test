using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class MobileForgotPasswordDataPacket : FlexiFlowDataPacketWithDtoBridge<MobileForgotPasswordDto, FlexAppContextBridge>
    {
        protected readonly ILogger<MobileForgotPasswordDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public MobileForgotPasswordDataPacket(ILogger<MobileForgotPasswordDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string? Key { get; set; }

        #endregion "Properties
    }
}