using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ValidateAgentDataPacket : FlexiFlowDataPacketWithDtoBridge<ValidateAgentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ValidateAgentDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ValidateAgentDataPacket(ILogger<ValidateAgentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public List<AgentDto> OutputDto { get; set; }

        #endregion "Properties
    }
}