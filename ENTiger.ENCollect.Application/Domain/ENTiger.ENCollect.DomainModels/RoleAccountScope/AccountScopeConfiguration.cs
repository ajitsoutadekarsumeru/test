using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AccountScopeConfiguration : DomainModelBridge
    {
        protected readonly ILogger<AccountScopeConfiguration> _logger;

        protected AccountScopeConfiguration()
        {
            _logger = FlexContainer.ServiceProvider.GetRequiredService<ILogger<AccountScopeConfiguration>>();
        }

        public AccountScopeConfiguration(ILogger<AccountScopeConfiguration> logger)
        {
            _logger = logger;
        }

        #region "Attributes"

        #region "Public"
        
        [StringLength(32)]
        public string AccountabilityTypeId { get; set; }
        public AccountabilityType AccountabilityType { get; set; }
        // Represents the role or entity that is accountable for access (e.g., "FieldAgent", "BankStaff").

        public string Scope { get; set; } = AccountAccessScopeEnum.All.Value;
        // Defines the scope of account access: "all" (fetch all), "parent" (fetch parent agency/basebranch accounts), "self" (fetch self-allocated accounts).

        public int ScopeLevel { get; set; } = 0;
        // Defines priority or level for role-based fetching (e.g., 1 for "self", 2 for "parent", 3 for "all").
        #endregion

        #region "Protected"
        #endregion

        #region "Private"
        #endregion

        #endregion

        #region "Private Methods"
        #endregion

    }
}
