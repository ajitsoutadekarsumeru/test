using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryUnAllocationByBatchDataPacket : FlexiFlowDataPacketWithDtoBridge<SecondaryUnAllocationByBatchDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SecondaryUnAllocationByBatchDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SecondaryUnAllocationByBatchDataPacket(ILogger<SecondaryUnAllocationByBatchDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}