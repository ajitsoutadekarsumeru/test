using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RejectAgentDataPacket : FlexiFlowDataPacketWithDtoBridge<RejectAgentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<RejectAgentDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public RejectAgentDataPacket(ILogger<RejectAgentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}