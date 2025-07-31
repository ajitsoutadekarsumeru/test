using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetAzureDataPacket : FlexiFlowDataPacketWithDtoBridge<GetAzureDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetAzureDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetAzureDataPacket(ILogger<GetAzureDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public GetAzureResultModel OutputDto { get; set; }

        #endregion "Properties
    }
}