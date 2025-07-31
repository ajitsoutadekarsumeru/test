using ENTiger.ENCollect.DomainModels;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;

namespace ENTiger.ENCollect.CollectionsModule
{
    public partial class WalletConsumption : IWalletConsumption
    {
        protected readonly ILogger<WalletConsumption> _logger;
        protected string EventCondition = "";  //event condition
        protected readonly IRepoFactory _repoFactory;
        protected readonly ICustomUtility _customUtility;
        protected FlexAppContextBridge? _flexAppContext;
        private readonly IWalletRepository _walletRepository;

        public WalletConsumption(ILogger<WalletConsumption> logger, 
            IRepoFactory repoFactory, 
            ICustomUtility customUtility,
            IWalletRepository walletRepository)
        {
            _logger = logger;
            _repoFactory = repoFactory;
            _customUtility = customUtility;
            _walletRepository = walletRepository;
        }

        public virtual async Task Execute(CollectionAddedEvent @event, IFlexServiceBusContext serviceBusContext)
        {
            _flexAppContext = @event.AppContext; //do not remove this line
            _repoFactory.Init(@event);

            if (!string.IsNullOrEmpty(@event.ReservationId))
            {
                _logger.LogInformation("WalletConsumption : Start");
                string loggedInUser = _flexAppContext.UserId;

                Wallet? wallet = await _walletRepository.GetByAgentIdAsync(_flexAppContext, loggedInUser);

                wallet.ConsumeFunds(@event.ReservationId);
                await _walletRepository.SaveAsync(_flexAppContext, wallet);

                _logger.LogInformation("WalletConsumption : End");
            }
            await Task.CompletedTask;
        }
    }
}