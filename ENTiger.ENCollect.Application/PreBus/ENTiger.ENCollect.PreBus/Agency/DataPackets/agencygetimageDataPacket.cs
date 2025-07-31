using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AgencyModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class agencygetimageDataPacket : FlexiFlowDataPacketWithDtoBridge<agencygetimageDto, FlexAppContextBridge>
    {
        protected readonly ILogger<agencygetimageDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public agencygetimageDataPacket(ILogger<agencygetimageDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}