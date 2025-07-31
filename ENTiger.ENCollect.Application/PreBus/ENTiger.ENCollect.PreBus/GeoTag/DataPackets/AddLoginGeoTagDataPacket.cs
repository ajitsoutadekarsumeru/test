using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddLoginGeoTagDataPacket : FlexiFlowDataPacketWithDtoBridge<AddLoginGeoTagDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddLoginGeoTagDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddLoginGeoTagDataPacket(ILogger<AddLoginGeoTagDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}