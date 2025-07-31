using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ChangePasswordDataPacket : FlexiFlowDataPacketWithDtoBridge<ChangePasswordDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ChangePasswordDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ChangePasswordDataPacket(ILogger<ChangePasswordDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string? Key { get; set; }

        #endregion "Properties
    }
}