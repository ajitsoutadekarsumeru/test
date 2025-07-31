using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateStatusDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateStatusDto, FlexAppContextBridge>
    {

        protected readonly ILogger<UpdateStatusDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateStatusDataPacket(ILogger<UpdateStatusDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
