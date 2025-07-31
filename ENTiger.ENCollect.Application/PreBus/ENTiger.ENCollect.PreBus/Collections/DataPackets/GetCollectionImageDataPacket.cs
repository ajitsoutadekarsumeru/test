using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetCollectionImageDataPacket : FlexiFlowDataPacketWithDtoBridge<GetCollectionImageDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetCollectionImageDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetCollectionImageDataPacket(ILogger<GetCollectionImageDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}