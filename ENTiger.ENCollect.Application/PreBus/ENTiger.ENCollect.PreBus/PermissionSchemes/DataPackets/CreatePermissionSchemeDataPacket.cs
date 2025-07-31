using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.PermissionSchemesModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CreatePermissionSchemeDataPacket : FlexiFlowDataPacketWithDtoBridge<CreatePermissionSchemeDto, FlexAppContextBridge>
    {

        protected readonly ILogger<CreatePermissionSchemeDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CreatePermissionSchemeDataPacket(ILogger<CreatePermissionSchemeDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
