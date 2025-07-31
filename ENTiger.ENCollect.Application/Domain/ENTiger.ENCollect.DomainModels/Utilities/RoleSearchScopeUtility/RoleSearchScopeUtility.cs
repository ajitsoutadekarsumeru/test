using Microsoft.EntityFrameworkCore;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    public class RoleSearchScopeUtility : IRoleSearchScopeUtility
    {
        private readonly IRepoFactory _repoFactory;
        private string _userId;
        private string? _parentId;

        public RoleSearchScopeUtility(
            IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }
        public async Task<string> GetRoleScopeInfo(string userId)
        {
            _userId = userId;
            // Fetch user AccountabilityTypeIds
            List<string> userAccountabilityTypeIds = await GetUserAccountabilityTypeIds();

            // Fetch RoleAccountScope for user AccountabilityTypeIds
            var roleScope = await FetchRoleScope(userAccountabilityTypeIds);

            var scope = roleScope?.Scope ?? AccountAccessScopeEnum.All.DisplayName;
            // Default to "All" if null

            return (scope);

        }

        private async Task<List<string>> GetUserAccountabilityTypeIds()
        {
            var Ids = await _repoFactory.GetRepo().FindAll<Accountability>()
                                .Where(a => a.ResponsibleId == _userId)
                                .Select(b => b.AccountabilityTypeId)
                                .ToListAsync();
            return Ids;
        }

        private async Task FetchUserParentId()
        {
            ApplicationUser? user = await _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(a => a.Id == _userId).FirstOrDefaultAsync();
            if (user?.GetType() == typeof(AgencyUser))
            {
                _parentId = await _repoFactory.GetRepo().FindAll<AgencyUser>()
                                        .Where(a => a.Id == _userId)
                                        .Select(s => s.AgencyId)
                                        .FirstOrDefaultAsync();
            }
            if (user?.GetType() == typeof(CompanyUser))
            {
                _parentId = await _repoFactory.GetRepo().FindAll<CompanyUser>()
                                                .Where(a => a.Id == _userId)
                                                .Select(s => s.BaseBranchId)
                                                .FirstOrDefaultAsync();
            }
        }

        private async Task<AccountScopeConfiguration> FetchRoleScope(List<string> userAccountabilityTypeIds)
        {
            var roleScope = await _repoFactory.GetRepo().FindAll<AccountScopeConfiguration>()
                                    .Where(r => userAccountabilityTypeIds.Contains(r.AccountabilityTypeId))
                                    .OrderBy(r => r.ScopeLevel)
                                    .FirstAsync();
            return roleScope;
        }
    }
}
