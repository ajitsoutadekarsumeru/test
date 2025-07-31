using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AzureLoginDataPacket : FlexiFlowDataPacketWithDtoBridge<AzureLoginDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AzureLoginDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AzureLoginDataPacket(ILogger<AzureLoginDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string? Key { get; set; }

        public TokenDto OutputDto { get; set; }

        #endregion "Properties
    }
}