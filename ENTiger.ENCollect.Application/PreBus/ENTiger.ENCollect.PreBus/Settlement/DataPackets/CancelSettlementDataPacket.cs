using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CancelSettlementDataPacket : FlexiFlowDataPacketWithDtoBridge<CancelSettlementDto, FlexAppContextBridge>
    {

        protected readonly ILogger<CancelSettlementDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CancelSettlementDataPacket(ILogger<CancelSettlementDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
