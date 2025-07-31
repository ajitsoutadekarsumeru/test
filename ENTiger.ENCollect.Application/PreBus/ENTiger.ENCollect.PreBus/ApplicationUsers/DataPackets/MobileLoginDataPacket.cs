using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class MobileLoginDataPacket : FlexiFlowDataPacketWithDtoBridge<MobileLoginDto, FlexAppContextBridge>
    {
        protected readonly ILogger<MobileLoginDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public MobileLoginDataPacket(ILogger<MobileLoginDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string? Key { get; set; }

        #endregion "Properties
    }
}