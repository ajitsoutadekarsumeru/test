using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ValidateAgentEmailDataPacket : FlexiFlowDataPacketWithDtoBridge<ValidateAgentEmailDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ValidateAgentEmailDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ValidateAgentEmailDataPacket(ILogger<ValidateAgentEmailDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public List<AgentEmailDto> OutputDto { get; set; }

        #endregion "Properties
    }
}