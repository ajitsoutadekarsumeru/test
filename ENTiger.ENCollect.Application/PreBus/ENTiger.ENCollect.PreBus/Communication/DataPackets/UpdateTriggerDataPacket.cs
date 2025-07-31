using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommunicationModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateTriggerDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateTriggerDto, FlexAppContextBridge>
    {

        protected readonly ILogger<UpdateTriggerDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateTriggerDataPacket(ILogger<UpdateTriggerDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
