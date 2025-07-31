using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class MastersImportDataPacket : FlexiFlowDataPacketWithDtoBridge<MastersImportDto, FlexAppContextBridge>
    {
        protected readonly ILogger<MastersImportDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public MastersImportDataPacket(ILogger<MastersImportDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}