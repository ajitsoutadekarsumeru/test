using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreateUsersByBatchDataPacket : FlexiFlowDataPacketWithDtoBridge<CreateUsersByBatchDto, FlexAppContextBridge>
    {
        protected readonly ILogger<CreateUsersByBatchDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public CreateUsersByBatchDataPacket(ILogger<CreateUsersByBatchDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}