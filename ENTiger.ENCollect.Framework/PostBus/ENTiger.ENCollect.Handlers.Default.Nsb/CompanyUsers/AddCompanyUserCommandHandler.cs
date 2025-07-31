using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddCompanyUserCommandHandler : NsbCommandHandler<AddCompanyUserCommand>
    {
        readonly ILogger<AddCompanyUserCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddCompanyUserCommandHandler(ILogger<AddCompanyUserCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddCompanyUserCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddCompanyUserCommandHandler: {nameof(AddCompanyUserCommandHandler)}");

            await this.ProcessHandlerSequence<AddCompanyUserPostBusDataPacket, AddCompanyUserPostBusSequence, 
                AddCompanyUserCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
