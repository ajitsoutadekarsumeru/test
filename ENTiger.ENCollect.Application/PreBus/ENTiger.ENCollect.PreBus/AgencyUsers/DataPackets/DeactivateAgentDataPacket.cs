using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class DeactivateAgentDataPacket : FlexiFlowDataPacketWithDtoBridge<DeactivateAgentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<DeactivateAgentDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public DeactivateAgentDataPacket(ILogger<DeactivateAgentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}