using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.CompanyUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateCompanyUserCommandHandler : NsbCommandHandler<UpdateCompanyUserCommand>
    {
        readonly ILogger<UpdateCompanyUserCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public UpdateCompanyUserCommandHandler(ILogger<UpdateCompanyUserCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(UpdateCompanyUserCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing UpdateCompanyUserCommandHandler: {nameof(UpdateCompanyUserCommandHandler)}");

            await this.ProcessHandlerSequence<UpdateCompanyUserPostBusDataPacket, UpdateCompanyUserPostBusSequence, 
                UpdateCompanyUserCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
