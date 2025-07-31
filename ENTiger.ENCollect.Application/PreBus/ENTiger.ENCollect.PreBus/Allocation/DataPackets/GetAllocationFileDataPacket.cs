using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetAllocationFileDataPacket : FlexiFlowDataPacketWithDtoBridge<GetAllocationFileDto, FlexAppContextBridge>
    {

        protected readonly ILogger<GetAllocationFileDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetAllocationFileDataPacket(ILogger<GetAllocationFileDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string FilePath { get; set; }

        #endregion
    }
}
