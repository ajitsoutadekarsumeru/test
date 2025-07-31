using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetImageDataPacket : FlexiFlowDataPacketWithDtoBridge<GetImageDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetImageDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetImageDataPacket(ILogger<GetImageDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}