using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EnableCompanyUsersDataPacket : FlexiFlowDataPacketWithDtoBridge<EnableCompanyUsersDto, FlexAppContextBridge>
    {

        protected readonly ILogger<EnableCompanyUsersDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public EnableCompanyUsersDataPacket(ILogger<EnableCompanyUsersDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
