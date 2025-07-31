using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateSettlementDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateSettlementDto, FlexAppContextBridge>
    {

        protected readonly ILogger<UpdateSettlementDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateSettlementDataPacket(ILogger<UpdateSettlementDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
