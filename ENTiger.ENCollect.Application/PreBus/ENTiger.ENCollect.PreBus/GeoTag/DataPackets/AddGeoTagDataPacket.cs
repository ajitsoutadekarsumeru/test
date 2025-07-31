using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddGeoTagDataPacket : FlexiFlowDataPacketWithDtoBridge<AddGeoTagDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddGeoTagDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddGeoTagDataPacket(ILogger<AddGeoTagDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}