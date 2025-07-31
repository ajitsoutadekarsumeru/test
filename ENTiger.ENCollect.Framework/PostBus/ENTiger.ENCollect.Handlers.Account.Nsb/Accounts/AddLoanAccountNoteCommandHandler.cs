using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddLoanAccountNoteCommandHandler : NsbCommandHandler<AddLoanAccountNoteCommand>
    {
        readonly ILogger<AddLoanAccountNoteCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddLoanAccountNoteCommandHandler(ILogger<AddLoanAccountNoteCommandHandler> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Handle(AddLoanAccountNoteCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddLoanAccountNoteCommandHandler: {nameof(AddLoanAccountNoteCommandHandler)}");

            await this.ProcessHandlerSequence<AddLoanAccountNotePostBusDataPacket, AddLoanAccountNotePostBusSequence, 
                AddLoanAccountNoteCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
