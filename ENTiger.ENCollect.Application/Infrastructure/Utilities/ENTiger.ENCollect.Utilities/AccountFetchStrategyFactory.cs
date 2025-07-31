using Microsoft.Extensions.DependencyInjection;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class AccountFetchStrategyFactory : IFlexUtilityService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<TriggerConditionTypeEnum, Type> _triggerTypeMappings;
        private readonly IDictionary<string, IAccountFetchStrategy> _strategies;
        public AccountFetchStrategyFactory(IServiceProvider serviceProvider, IEnumerable<IAccountFetchStrategy> strategies)
        {
            _serviceProvider = serviceProvider;
            _strategies = strategies.ToDictionary(s => s.SupportedTriggerType.ToLowerInvariant(), s => s);
        }
        public IAccountFetchStrategy Get(string triggerType)
     => _strategies.TryGetValue(triggerType.ToLowerInvariant(), out var strategy)
         ? strategy
         : throw new InvalidOperationException($"No strategy for trigger type {triggerType}");

    }
}