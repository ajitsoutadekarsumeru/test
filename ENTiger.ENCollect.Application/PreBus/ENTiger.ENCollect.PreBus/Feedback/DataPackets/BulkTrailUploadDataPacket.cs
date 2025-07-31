using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.FeedbackModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class BulkTrailUploadDataPacket : FlexiFlowDataPacketWithDtoBridge<BulkTrailUploadDto, FlexAppContextBridge>
    {
        protected readonly ILogger<BulkTrailUploadDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public BulkTrailUploadDataPacket(ILogger<BulkTrailUploadDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}