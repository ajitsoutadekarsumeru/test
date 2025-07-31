using System.Threading.Tasks;
using NServiceBus;
using Sumeru.Flex;
using Microsoft.Extensions.Logging;
using ENTiger.ENCollect.ApplicationUsersModule;
using ENTiger.ENCollect.AgencyUsersModule;
using ENCollect.Dyna.Workflows;

namespace ENTiger.ENCollect.SettlementModule
{
    /// <summary>
    /// 
    /// </summary>
    public class GenerateSettlementLetterBusGammaSubscriber : NsbSubscriberBridge<SettlementApprovedEvent>
    {
        readonly ILogger<GenerateSettlementLetterBusGammaSubscriber> _logger;
        readonly IGenerateSettlementLetter _subscriber;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public GenerateSettlementLetterBusGammaSubscriber(ILogger<GenerateSettlementLetterBusGammaSubscriber> logger,
            IGenerateSettlementLetter subscriber)
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
        public override async Task Handle(SettlementApprovedEvent message, IMessageHandlerContext context)
        {
            await _subscriber.Execute(message, new NsbHandlerContextBridge(context));
        }
    }
}
