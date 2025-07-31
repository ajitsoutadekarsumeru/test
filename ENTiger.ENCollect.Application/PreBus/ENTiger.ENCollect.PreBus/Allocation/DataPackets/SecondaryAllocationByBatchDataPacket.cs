using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryAllocationByBatchDataPacket : FlexiFlowDataPacketWithDtoBridge<SecondaryAllocationByBatchDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SecondaryAllocationByBatchDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SecondaryAllocationByBatchDataPacket(ILogger<SecondaryAllocationByBatchDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}