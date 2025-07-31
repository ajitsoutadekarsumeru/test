using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class RenewAgentDataPacket : FlexiFlowDataPacketWithDtoBridge<RenewAgentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<RenewAgentDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public RenewAgentDataPacket(ILogger<RenewAgentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}