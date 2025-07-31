using Sumeru.Flex;
using System;
using System.ComponentModel.DataAnnotations;

namespace ENTiger.ENCollect.AccountsModule
{
    /// <summary>
    /// 
    /// </summary>
    public partial class GetRoleBasedSearchConfigDto : DtoBridge
    {
        [StringLength(32)]
        public string Id { get; set; }

        public string UserType { get; set; }

        public string Scope { get; set; }
        // Defines the scope of account access: "all" (fetch all), "parent" (fetch parent agency/basebranch accounts), "self" (fetch self-allocated accounts).

        public int ScopeLevel { get; set; } = 0;
    }
}
