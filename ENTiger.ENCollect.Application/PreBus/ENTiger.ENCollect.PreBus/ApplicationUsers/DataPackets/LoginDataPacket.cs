using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoginDataPacket : FlexiFlowDataPacketWithDtoBridge<LoginDto, FlexAppContextBridge>
    {
        protected readonly ILogger<LoginDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public LoginDataPacket(ILogger<LoginDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string? Key { get; set; }

        public TokenDto token { get; set; }

        #endregion "Properties
    }
}