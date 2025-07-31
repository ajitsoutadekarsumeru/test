using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.GeoTagModule
{
    /// <summary>
    /// Command handler responsible for processing the GenerateGeoReportCommand.
    /// It orchestrates the post-bus processing sequence for geo report generation.
    /// </summary>
    public class GenerateGeoReportCommandHandler : NsbCommandHandler<GenerateGeoReportCommand>
    {
        private readonly ILogger<GenerateGeoReportCommandHandler> _logger;
        private readonly IFlexHost _flexHost;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenerateGeoReportCommandHandler"/> class.
        /// </summary>
        /// <param name="logger">The logger for diagnostic and operational messages.</param>
        /// <param name="flexHost">The host context used during processing.</param>
        public GenerateGeoReportCommandHandler(
            ILogger<GenerateGeoReportCommandHandler> logger,
            IFlexHost flexHost)
        {
            _logger = logger;
            _flexHost = flexHost;
        }

        /// <summary>
        /// Handles the execution of the geo report generation command.
        /// </summary>
        /// <param name="message">The command message containing report generation input.</param>
        /// <param name="context">The message handling context provided by NServiceBus.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public override async Task Handle(GenerateGeoReportCommand message, IMessageHandlerContext context)
        {
            _logger.LogTrace("Handling command {CommandName} in {Handler}.",
                nameof(GenerateGeoReportCommand), nameof(GenerateGeoReportCommandHandler));

            await ProcessHandlerSequence<GenerateGeoReportPostBusDataPacket, GenerateGeoReportPostBusSequence,
                GenerateGeoReportCommand, NsbHandlerContextBridge>(message, new NsbHandlerContextBridge(context));

            _logger.LogInformation("Geo report command processed successfully.");
        }
    }
}
