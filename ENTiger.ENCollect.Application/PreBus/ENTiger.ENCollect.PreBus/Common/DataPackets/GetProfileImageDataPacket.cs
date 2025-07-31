using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetProfileImageDataPacket : FlexiFlowDataPacketWithDtoBridge<GetProfileImageDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetProfileImageDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetProfileImageDataPacket(ILogger<GetProfileImageDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}