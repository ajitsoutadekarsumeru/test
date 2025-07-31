using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddAgentDataPacket : FlexiFlowDataPacketWithDtoBridge<AddAgentDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddAgentDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddAgentDataPacket(ILogger<AddAgentDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}