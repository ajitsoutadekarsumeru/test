using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;

namespace ENTiger.ENCollect.CommonModule
{
    /// <summary>
    /// 
    /// </summary>
    public class ProcessUpdateUsersUploadedBusGammaSubscriber : NsbSubscriberBridge<UpdateUsersUploadedEvent>
    {
        readonly ILogger<ProcessUpdateUsersUploadedBusGammaSubscriber> _logger;
        readonly IProcessUpdateUsersUploaded _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ProcessUpdateUsersUploadedBusGammaSubscriber(ILogger<ProcessUpdateUsersUploadedBusGammaSubscriber> logger, IProcessUpdateUsersUploaded subscriber)
        {
            _logger = logger;
            _subscriber = subscriber;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task Handle(UpdateUsersUploadedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
