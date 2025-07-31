using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class RunTriggersDataPacket : FlexiFlowDataPacketWithDtoBridge<RunTriggersDto, FlexAppContextBridge>
    {

        protected readonly ILogger<RunTriggersDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public RunTriggersDataPacket(ILogger<RunTriggersDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
