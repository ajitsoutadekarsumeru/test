using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApproveAgentDataPacket : FlexiFlowDataPacketWithDtoBridge<ApproveAgentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<ApproveAgentDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public ApproveAgentDataPacket(ILogger<ApproveAgentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}