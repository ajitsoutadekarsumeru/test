using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAgentImageDataPacket : FlexiFlowDataPacketWithDtoBridge<GetAgentImageDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetAgentImageDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetAgentImageDataPacket(ILogger<GetAgentImageDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}