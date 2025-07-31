using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class PrimaryAllocationByBatchDataPacket : FlexiFlowDataPacketWithDtoBridge<PrimaryAllocationByBatchDto, FlexAppContextBridge>
    {
        protected readonly ILogger<PrimaryAllocationByBatchDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public PrimaryAllocationByBatchDataPacket(ILogger<PrimaryAllocationByBatchDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}