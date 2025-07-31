using ENTiger.ENCollect.CommonModule;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyUsersModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UploadDataPacket : FlexiFlowDataPacketWithDtoBridge<UploadDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UploadDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UploadDataPacket(ILogger<UploadDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public FileDto OutputDto { get; set; }

        #endregion "Properties
    }
}