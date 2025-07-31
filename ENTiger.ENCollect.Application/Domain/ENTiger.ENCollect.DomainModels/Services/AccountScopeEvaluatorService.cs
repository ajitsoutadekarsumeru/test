namespace ENTiger.ENCollect
{
    public class AccountScopeEvaluatorService
    {
        private readonly IApplicationUserQueryRepository _applicationUserQueryRepository;

        public AccountScopeEvaluatorService(
            IApplicationUserQueryRepository applicationUserQueryRepository)
        {
            _applicationUserQueryRepository = applicationUserQueryRepository;
        }

        /// <summary>
        /// Computes the effective scope based on scope configurations and accountabilities.
        /// When the effective scope is Parent, it retrieves the parent's id from the accountabilities.
        /// </summary>
        public async Task<EffectiveScope> EvaluateScope(List<Accountability> accountabilities, 
            List<AccountScopeConfiguration> scopeConfigs, string userId, FlexAppContextBridge context)
        {
            if (scopeConfigs == null || scopeConfigs.Count == 0)
            {
                return new EffectiveScope(new AccountScopeFilter(AccountAccessScopeEnum.All.DisplayName), null);
            }

            // Determine the effective configuration based on the lowest ScopeLevel.
            AccountScopeConfiguration effectiveConfig = scopeConfigs[0];
            foreach (var config in scopeConfigs)
            {
                if (config.ScopeLevel < effectiveConfig.ScopeLevel)
                {
                    effectiveConfig = config;
                }
            }

            string? parentId = null;
            if (effectiveConfig.Scope.ToLower() == AccountAccessScopeEnum.Parent.DisplayName.ToLower())
            {
                // Retrieve parent's id from accountabilities. Assume at least one accountability has a ParentId.
                parentId = await _applicationUserQueryRepository.GetParentIdByUserId(userId, context);
            }

            return new EffectiveScope(new AccountScopeFilter(effectiveConfig.Scope), parentId);
        }

       
    }
}
