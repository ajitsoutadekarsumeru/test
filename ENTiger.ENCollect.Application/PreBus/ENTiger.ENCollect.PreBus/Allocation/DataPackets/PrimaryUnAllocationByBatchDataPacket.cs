using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class PrimaryUnAllocationByBatchDataPacket : FlexiFlowDataPacketWithDtoBridge<PrimaryUnAllocationByBatchDto, FlexAppContextBridge>
    {
        protected readonly ILogger<PrimaryUnAllocationByBatchDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public PrimaryUnAllocationByBatchDataPacket(ILogger<PrimaryUnAllocationByBatchDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}