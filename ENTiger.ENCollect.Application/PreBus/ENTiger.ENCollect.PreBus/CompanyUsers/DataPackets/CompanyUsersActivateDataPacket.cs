using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CompanyUsersActivateDataPacket : FlexiFlowDataPacketWithDtoBridge<CompanyUsersActivateDto, FlexAppContextBridge>
    {

        protected readonly ILogger<CompanyUsersActivateDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public CompanyUsersActivateDataPacket(ILogger<CompanyUsersActivateDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
