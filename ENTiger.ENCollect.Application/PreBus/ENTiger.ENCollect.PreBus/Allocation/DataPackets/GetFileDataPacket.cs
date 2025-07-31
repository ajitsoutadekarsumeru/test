using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetFileDataPacket : FlexiFlowDataPacketWithDtoBridge<GetFileDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetFileDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetFileDataPacket(ILogger<GetFileDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        public string FilePath { get; set; }


        //Models and other properties goes here

        #endregion "Properties
    }
}