using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class IkontelDataPacket : FlexiFlowDataPacketWithDtoBridge<IkontelDto, FlexAppContextBridge>
    {
        protected readonly ILogger<IkontelDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public IkontelDataPacket(ILogger<IkontelDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}