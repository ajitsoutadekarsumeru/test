using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PublicModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class ImportCollectionsDataPacket : FlexiFlowDataPacketWithDtoBridge<ImportCollectionsDto, FlexAppContextBridge>
    {

        protected readonly ILogger<ImportCollectionsDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ImportCollectionsDataPacket(ILogger<ImportCollectionsDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
