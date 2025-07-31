using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.DesignationsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AssignSchemeDataPacket : FlexiFlowDataPacketWithDtoBridge<AssignSchemeDto, FlexAppContextBridge>
    {

        protected readonly ILogger<AssignSchemeDataPacket> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AssignSchemeDataPacket(ILogger<AssignSchemeDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here


        #endregion
    }
}
