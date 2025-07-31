using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetCollectionFileDataPacket : FlexiFlowDataPacketWithDtoBridge<GetCollectionFileDto, FlexAppContextBridge>
    {

        protected readonly ILogger<GetCollectionFileDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GetCollectionFileDataPacket(ILogger<GetCollectionFileDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string FilePath { get; set; }

        #endregion
    }
}
