using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class LogoutDataPacket : FlexiFlowDataPacketWithDtoBridge<LogoutDto, FlexAppContextBridge>
    {
        protected readonly ILogger<LogoutDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public LogoutDataPacket(ILogger<LogoutDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}