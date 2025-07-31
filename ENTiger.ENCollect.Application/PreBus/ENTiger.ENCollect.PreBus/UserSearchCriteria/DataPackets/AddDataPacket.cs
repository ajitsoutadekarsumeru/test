using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.UserSearchCriteriaModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddDataPacket : FlexiFlowDataPacketWithDtoBridge<AddDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddDataPacket(ILogger<AddDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}