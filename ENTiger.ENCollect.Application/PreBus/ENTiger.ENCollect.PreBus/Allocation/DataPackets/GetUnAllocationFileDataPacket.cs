using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetUnAllocationFileDataPacket : FlexiFlowDataPacketWithDtoBridge<GetUnAllocationFileDto, FlexAppContextBridge>
    {

        protected readonly ILogger<GetUnAllocationFileDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetUnAllocationFileDataPacket(ILogger<GetUnAllocationFileDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string FilePath { get; set; }

        #endregion
    }
}
