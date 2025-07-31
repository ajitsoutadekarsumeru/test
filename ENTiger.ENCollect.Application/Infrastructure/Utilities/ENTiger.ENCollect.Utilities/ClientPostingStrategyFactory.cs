using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class ClientPostingStrategyFactory : IFlexUtilityService
    {
        private readonly ClientCBSPostingStrategy _clientCBSPostingStrategy;

        public ClientPostingStrategyFactory(ClientCBSPostingStrategy clientCBSPostingStrategy)
        {
            _clientCBSPostingStrategy = clientCBSPostingStrategy;
        }

        public virtual IClientPostingStrategy GetStrategy()
        {
            return _clientCBSPostingStrategy;
        }
    }
}
