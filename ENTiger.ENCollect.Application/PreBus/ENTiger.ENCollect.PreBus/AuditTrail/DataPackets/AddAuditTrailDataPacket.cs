using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AuditTrailModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AddAuditTrailDataPacket : FlexiFlowDataPacketWithDtoBridge<AddAuditTrailDto, FlexAppContextBridge>
    {

        protected readonly ILogger<AddAuditTrailDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddAuditTrailDataPacket(ILogger<AddAuditTrailDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
