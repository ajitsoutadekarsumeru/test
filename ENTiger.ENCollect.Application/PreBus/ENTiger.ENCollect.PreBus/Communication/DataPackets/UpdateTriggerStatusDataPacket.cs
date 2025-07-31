using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateTriggerStatusDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateTriggerStatusDto, FlexAppContextBridge>
    {

        protected readonly ILogger<UpdateTriggerStatusDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateTriggerStatusDataPacket(ILogger<UpdateTriggerStatusDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
