using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdatePayInSlipDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdatePayInSlipDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdatePayInSlipDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdatePayInSlipDataPacket(ILogger<UpdatePayInSlipDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}