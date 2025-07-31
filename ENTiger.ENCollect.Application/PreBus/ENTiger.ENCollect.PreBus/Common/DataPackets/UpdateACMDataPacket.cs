using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateACMDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateACMDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateACMDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateACMDataPacket(ILogger<UpdateACMDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}