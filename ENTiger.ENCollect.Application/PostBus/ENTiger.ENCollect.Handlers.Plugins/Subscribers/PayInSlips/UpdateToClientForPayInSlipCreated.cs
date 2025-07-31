using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.PayInSlipsModule
{
    /// <summary>
    /// Handles the event when a new PayInSlip is created and updates the client accordingly.
    /// </summary>
    public partial class UpdateToClientForPayInSlipCreated : IUpdateToClientForPayInSlipCreated
    {
        private readonly ILogger<UpdateToClientForPayInSlipCreated> _logger;
        private readonly IRepoFactory _repositoryFactory;
        private readonly IFlexTenantRepository<FlexTenantBridge> _tenantRepository;
        private readonly IFlexHost _flexHost;

        public UpdateToClientForPayInSlipCreated(
            ILogger<UpdateToClientForPayInSlipCreated> logger,
            IRepoFactory repositoryFactory,
            IFlexHost flexHost,
            IFlexTenantRepository<FlexTenantBridge> tenantRepository)
        {
            _logger = logger;
            _repositoryFactory = repositoryFactory;
            _tenantRepository = tenantRepository;
            _flexHost = flexHost;
        }

        /// <summary>
        /// Processes the PayInSlipCreated event and triggers client updates if necessary.
        /// </summary>
        public async Task Execute(PayInSlipCreatedEvent eventData, IFlexServiceBusContext serviceBusContext)
        {
            string? tenantId = eventData.AppContext?.TenantId;
            _logger.LogInformation("Processing PayInSlipCreatedEvent | PayInSlipId: {PayInSlipId}, TenantId: {TenantId}", eventData.Id, tenantId);

            try
            {
                var repo = _repositoryFactory.Init(eventData);
                var payInSlipPostingEnableConfig = await repo.GetRepo()
                                                       .FindAll<FeatureMaster>()
                                                       .FirstOrDefaultAsync(p => p.Parameter == "PayInSlipPostingEnable");

                bool IsPayInSlipPostingEnabled = bool.TryParse(payInSlipPostingEnableConfig?.Value, out var enabled) && enabled;
               
                if (!IsPayInSlipPostingEnabled)
                {
                    _logger.LogInformation("Pay-in slip posting is disabled for TenantId: {TenantId}", tenantId);
                    return;
                }

                var payInSlipDetails = await repo.GetRepo()
                                        .FindAll<PayInSlip>()
                                        .ById(eventData.Id)
                                        .SelectTo<PayInSlipDtoWithId>()
                                        .SingleOrDefaultAsync();

                var paymentPostingFactory = _flexHost.GetUtilityService<ClientPostingStrategyFactory>();
                var postingStrategy = paymentPostingFactory.GetStrategy();

                payInSlipDetails?.SetAppContext(eventData.AppContext);

                await postingStrategy.PostPayInSlipAsync(payInSlipDetails);

                _logger.LogInformation("Successfully processed PayInSlipCreatedEvent | PayInSlipId: {PayInSlipId}, TenantId: {TenantId}", eventData.Id, tenantId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing PayInSlipCreatedEvent | PayInSlipId: {PayInSlipId}, TenantId: {TenantId}", eventData.Id, tenantId);
            }
        }
    }
}
