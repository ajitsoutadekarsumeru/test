using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdatePermissionSchemeDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdatePermissionSchemeDto, FlexAppContextBridge>
    {

        protected readonly ILogger<UpdatePermissionSchemeDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdatePermissionSchemeDataPacket(ILogger<UpdatePermissionSchemeDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
