using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;


namespace ENTiger.ENCollect.ApplicationUsersModule
{
    /// <summary>
    /// 
    /// </summary>
    public class AddUserAttendanceCommandHandler : NsbCommandHandler<AddUserAttendanceCommand>
    {
        readonly ILogger<AddUserAttendanceCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public AddUserAttendanceCommandHandler(ILogger<AddUserAttendanceCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(AddUserAttendanceCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing AddUserAttendanceCommandHandler: {nameof(AddUserAttendanceCommandHandler)}");

            await this.ProcessHandlerSequence<AddUserAttendancePostBusDataPacket, AddUserAttendancePostBusSequence, 
                AddUserAttendanceCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
