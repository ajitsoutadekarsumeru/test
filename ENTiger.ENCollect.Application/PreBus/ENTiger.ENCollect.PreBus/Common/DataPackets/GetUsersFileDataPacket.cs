using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class GetUsersFileDataPacket : FlexiFlowDataPacketWithDtoBridge<GetUsersFileDto, FlexAppContextBridge>
    {
        protected readonly ILogger<GetUsersFileDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public GetUsersFileDataPacket(ILogger<GetUsersFileDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here
        public string FilePath { get; set; }
        #endregion "Properties
    }
}