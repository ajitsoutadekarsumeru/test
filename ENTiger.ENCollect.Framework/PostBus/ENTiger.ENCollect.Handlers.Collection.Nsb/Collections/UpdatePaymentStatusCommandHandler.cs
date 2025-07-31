using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    /// <summary>
    /// Command handler for processing the <see cref="UpdatePaymentStatusCommand"/>.
    /// This handler initiates the execution of a predefined post-bus sequence to update payment statuses.
    /// </summary>
    public class UpdatePaymentStatusCommandHandler : NsbCommandHandler<UpdatePaymentStatusCommand>
    {
        private readonly ILogger<UpdatePaymentStatusCommandHandler> _logger;
        private readonly IFlexHost _flexHost;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdatePaymentStatusCommandHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger instance used for tracing and diagnostics.</param>
        /// <param name="flexHost">The host used to resolve utility services and configurations.</param>
        public UpdatePaymentStatusCommandHandler(ILogger<UpdatePaymentStatusCommandHandler> logger, IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        /// Handles the <see cref="UpdatePaymentStatusCommand"/> and initiates the sequence to update payment statuses.
        /// </summary>
        /// <param name="message">The command message containing the update request details.</param>
        /// <param name="context">The message handler context provided by NServiceBus.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task Handle(UpdatePaymentStatusCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace($"Executing {nameof(UpdatePaymentStatusCommandHandler)} for UpdatePaymentStatusCommand.");

            await this.ProcessHandlerSequence<
                UpdatePaymentStatusPostBusDataPacket,
                UpdatePaymentStatusPostBusSequence,
                UpdatePaymentStatusCommand,
                NsbHandlerContextBridge>(
                    message,
                    new NsbHandlerContextBridge(context)
                );
        }
    }
}
