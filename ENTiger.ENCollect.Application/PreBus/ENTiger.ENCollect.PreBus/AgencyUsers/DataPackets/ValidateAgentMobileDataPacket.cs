using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ValidateAgentMobileDataPacket : FlexiFlowDataPacketWithDtoBridge<ValidateAgentMobileDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ValidateAgentMobileDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ValidateAgentMobileDataPacket(ILogger<ValidateAgentMobileDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public List<AgentMobileDto> OutputDto { get; set; }

        #endregion "Properties
    }
}