using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class UpdateMastersDataPacket : FlexiFlowDataPacketWithDtoBridge<UpdateMastersDto, FlexAppContextBridge>
    {
        protected readonly ILogger<UpdateMastersDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public UpdateMastersDataPacket(ILogger<UpdateMastersDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}