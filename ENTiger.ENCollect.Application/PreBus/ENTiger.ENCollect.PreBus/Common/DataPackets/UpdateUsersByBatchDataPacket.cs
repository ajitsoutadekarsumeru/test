using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateUsersByBatchDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateUsersByBatchDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateUsersByBatchDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateUsersByBatchDataPacket(ILogger<UpdateUsersByBatchDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}