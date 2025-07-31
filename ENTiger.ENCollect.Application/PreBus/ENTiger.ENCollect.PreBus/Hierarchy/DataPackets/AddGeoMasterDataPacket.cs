using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.HierarchyModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AddGeoMasterDataPacket : FlexiFlowDataPacketWithDtoBridge<AddGeoMasterDto, FlexAppContextBridge>
    {

        protected readonly ILogger<AddGeoMasterDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddGeoMasterDataPacket(ILogger<AddGeoMasterDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
