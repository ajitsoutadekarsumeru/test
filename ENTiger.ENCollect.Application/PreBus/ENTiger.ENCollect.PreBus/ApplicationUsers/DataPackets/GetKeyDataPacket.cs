using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetKeyDataPacket : FlexiFlowDataPacketWithDtoBridge<GetKeyDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetKeyDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetKeyDataPacket(ILogger<GetKeyDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}