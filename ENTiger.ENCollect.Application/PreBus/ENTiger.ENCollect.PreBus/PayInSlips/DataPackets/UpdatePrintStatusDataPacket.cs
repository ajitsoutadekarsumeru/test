using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdatePrintStatusDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdatePrintStatusDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdatePrintStatusDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdatePrintStatusDataPacket(ILogger<UpdatePrintStatusDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}