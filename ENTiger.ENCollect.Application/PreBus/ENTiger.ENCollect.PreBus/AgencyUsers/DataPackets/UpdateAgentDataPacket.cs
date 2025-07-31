using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateAgentDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateAgentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateAgentDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateAgentDataPacket(ILogger<UpdateAgentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}