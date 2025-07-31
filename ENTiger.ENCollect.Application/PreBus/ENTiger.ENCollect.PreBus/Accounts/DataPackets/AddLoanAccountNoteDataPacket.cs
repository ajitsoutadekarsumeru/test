using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    ///
    /// </summary>
    public partial class AddLoanAccountNoteDataPacket : FlexiFlowDataPacketWithDtoBridge<AddLoanAccountNoteDto, FlexAppContextBridge>
    {
        protected readonly ILogger<AddLoanAccountNoteDataPacket> _logger;

        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public AddLoanAccountNoteDataPacket(ILogger<AddLoanAccountNoteDataPacket> logger)
        {
            _logger = logger;
        }

        #region "Properties

        //Models and other properties goes here

        #endregion "Properties
    }
}