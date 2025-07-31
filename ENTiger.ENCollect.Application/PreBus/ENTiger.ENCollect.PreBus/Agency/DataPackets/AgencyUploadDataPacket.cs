using ENTiger.ENCollect.CommonModule;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUploadDataPacket : FlexiFlowDataPacketWithDtoBridge<AgencyUploadDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AgencyUploadDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AgencyUploadDataPacket(ILogger<AgencyUploadDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public FileDto OutputDto { get; set; }

        #endregion "Properties
    }
}