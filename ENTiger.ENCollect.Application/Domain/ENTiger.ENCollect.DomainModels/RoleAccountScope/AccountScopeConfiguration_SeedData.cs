using Sumeru.Flex;
using System.Collections.Generic;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AccountScopeConfiguration : DomainModelBridge
    {
        public static IEnumerable<AccountScopeConfiguration> GetSeedData()
        {
            ICollection<AccountScopeConfiguration> seedData = new List<AccountScopeConfiguration>()
            {
                //add your object collection seed here:
                //field agent
                new AccountScopeConfiguration()
                {
                   Id = "3a185d8db599c016d4caf7aa05af889f",
                   AccountabilityTypeId = AccountabilityTypeEnum.AgencyToFrontEndExternalFOS.Value,
                   Scope = AccountAccessScopeEnum.All.DisplayName,
                   ScopeLevel = int.Parse(AccountAccessScopeEnum.All.Value),
                   IsDeleted = false,
                   CreatedDate = new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified)),
                   LastModifiedDate = new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified))
                },

                //field TCagent
                new AccountScopeConfiguration()
                {
                   Id = "3a185d8db599f4a83d63dec4faea8a98",
                   AccountabilityTypeId = AccountabilityTypeEnum.AgencyToFrontEndExternalTC.Value,
                   Scope = AccountAccessScopeEnum.All.DisplayName,
                   ScopeLevel = int.Parse(AccountAccessScopeEnum.All.Value),
                   IsDeleted = false,
                   CreatedDate = new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified)),
                   LastModifiedDate = new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified))
                },

                //field StaffFOS
                new AccountScopeConfiguration()
                {
                   Id = "3a185d8db599d1ce3ace0b1c74528678",
                   AccountabilityTypeId = AccountabilityTypeEnum.BankToFrontEndInternalFOS.Value,
                   Scope = AccountAccessScopeEnum.All.DisplayName,
                   ScopeLevel = int.Parse(AccountAccessScopeEnum.All.Value),
                   IsDeleted = false,
                   CreatedDate = new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified)),
                   LastModifiedDate = new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified))
                },

                //field StaffTC
                new AccountScopeConfiguration()
                {
                   Id = "3a185d8db599f686a3b157eaeb799b2d",
                   AccountabilityTypeId = AccountabilityTypeEnum.BankToFrontEndInternalTC.Value,
                   Scope = AccountAccessScopeEnum.All.DisplayName,
                   ScopeLevel = int.Parse(AccountAccessScopeEnum.All.Value),
                   IsDeleted = false,
                   CreatedDate = new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified)),
                   LastModifiedDate = new DateTimeOffset(new DateTime(2025, 7, 14, 15, 20, 29, 245, DateTimeKind.Unspecified))
                }
            };

            return seedData;
        }

    }
}
