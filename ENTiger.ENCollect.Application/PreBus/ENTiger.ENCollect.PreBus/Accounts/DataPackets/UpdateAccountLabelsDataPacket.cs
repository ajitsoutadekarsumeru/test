using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateAccountLabelsDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateAccountLabelsDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateAccountLabelsDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateAccountLabelsDataPacket(ILogger<UpdateAccountLabelsDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}