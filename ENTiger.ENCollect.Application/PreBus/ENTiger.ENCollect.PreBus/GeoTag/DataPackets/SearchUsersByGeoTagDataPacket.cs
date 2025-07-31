using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class SearchUsersByGeoTagDataPacket : FlexiFlowDataPacketWithDtoBridge<SearchUsersByGeoTagDto, FlexAppContextBridge>
    {
        protected readonly ILogger<SearchUsersByGeoTagDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public SearchUsersByGeoTagDataPacket(ILogger<SearchUsersByGeoTagDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public List<SearchUsersByGeoTagResultModel> output { get; set; }

        #endregion "Properties
    }
}