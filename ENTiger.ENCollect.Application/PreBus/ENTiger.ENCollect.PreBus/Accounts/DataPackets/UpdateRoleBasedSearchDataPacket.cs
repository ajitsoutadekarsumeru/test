using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateRoleBasedSearchDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateAccountScopeConfigurationDto, FlexAppContextBridge>
    {

        protected readonly ILogger<UpdateRoleBasedSearchDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateRoleBasedSearchDataPacket(ILogger<UpdateRoleBasedSearchDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
