using ENTiger.ENCollect.TreatmentModule;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTiger.ENCollect.Handlers.Default.Nsb.Treatment
{
    public class ExecuteFragmentedTreatmentCommandHandler : NsbCommandHandler<ExecuteFragmentedTreatmentCommand>
    {
        readonly ILogger<ExecuteFragmentedTreatmentCommandHandler> _logger;
        readonly IFlexHost _flexHost;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public ExecuteFragmentedTreatmentCommandHandler(ILogger<ExecuteFragmentedTreatmentCommandHandler> logger, IFlexHost flexHost)
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
        public override async Task Handle(ExecuteFragmentedTreatmentCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing ExecuteFragmentedTreatmentCommand: {nameof(ExecuteFragmentedTreatmentCommand)}");
           
            await this.ProcessHandlerSequence<ExecuteFragmentedTreatmentDataPacket, ExecuteFragmentedTreatmentPostBusFFSequence,
                ExecuteFragmentedTreatmentCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));
        }
    }
}
