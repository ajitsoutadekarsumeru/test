using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    public partial class UpdateAccountScopeConfigurationDto : DtoBridge
    {
        [Required]
        public List<AccountScopeConfigurationDto> SearchScopes { get; set; }
    }

    public partial class AccountScopeConfigurationDto : DtoBridge
    {
        [StringLength(32)]
        public string Id { get; set; }

        public string UserType { get; set; }

        public string Scope { get; set; }
        // Defines the scope of account access: "all" (fetch all), "parent" (fetch parent agency/basebranch accounts), "self" (fetch self-allocated accounts).

       
    }

}
