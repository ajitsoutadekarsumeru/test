using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UpdateCodeOfConductDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateCodeOfConductDto, FlexAppContextBridge>
    {

        protected readonly ILogger<UpdateCodeOfConductDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateCodeOfConductDataPacket(ILogger<UpdateCodeOfConductDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
