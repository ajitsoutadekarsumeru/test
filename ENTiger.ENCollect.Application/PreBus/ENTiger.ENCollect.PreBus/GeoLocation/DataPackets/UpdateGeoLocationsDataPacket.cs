using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.GeoLocationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateGeoLocationsDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateGeoLocationsDto, FlexAppContextBridge>
    {

        protected readonly ILogger<UpdateGeoLocationsDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateGeoLocationsDataPacket(ILogger<UpdateGeoLocationsDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
