using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UploadFileDataPacket : FlexiFlowDataPacketWithDtoBridge<UploadFileDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UploadFileDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UploadFileDataPacket(ILogger<UploadFileDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public FileDto OutputDto { get; set; }

        #endregion "Properties
    }
}