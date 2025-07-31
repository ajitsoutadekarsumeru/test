using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RequestSettlementDataPacket : FlexiFlowDataPacketWithDtoBridge<RequestSettlementDto, FlexAppContextBridge>
    {

        protected readonly ILogger<RequestSettlementDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RequestSettlementDataPacket(ILogger<RequestSettlementDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
