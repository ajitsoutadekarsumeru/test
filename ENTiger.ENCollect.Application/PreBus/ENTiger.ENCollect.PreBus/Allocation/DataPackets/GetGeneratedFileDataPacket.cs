using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AllocationModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetGeneratedFileDataPacket : FlexiFlowDataPacketWithDtoBridge<GetGeneratedFileDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetFileDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetGeneratedFileDataPacket(ILogger<GetFileDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        public string FilePath { get; set; }


        //Models and other properties goes here

        #endregion "Properties
    }
}