using Microsoft.EntityFrameworkCore;
using Sumeru.Flex;
using Microsoft.Extensions.Options;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Repository for querying application user-related data.
    /// </summary>
    public class ApplicationUserQueryRepository : IApplicationUserQueryRepository
    {
        private readonly IRepoFactory _repoFactory;


        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUserQueryRepository"/> class.
        /// </summary>
        /// <param name="repoFactory">The repository factory instance used to access the data store.</param>
        public ApplicationUserQueryRepository(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// Gets the parent ID (AgencyId or BaseBranchId) for a given user ID.
        /// </summary>
        /// <param name="userId">The user ID for which the parent ID is to be retrieved.</param>
        /// <param name="context">The context used to initialize the repository.</param>
        /// <returns>The parent ID as a string, or null if not applicable.</returns>
        public async Task<string> GetParentIdByUserId(string userId, FlexAppContextBridge context)
        {
            _repoFactory.Init(context);
            // Fetch the user once and check their type
            var user = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                        .FirstOrDefaultAsync(a => a.Id == userId);

            string _parentId;
            switch (user)
            {
                case AgencyUser agencyUser:
                    _parentId = agencyUser.AgencyId;
                    break;

                case CompanyUser companyUser:
                    _parentId = companyUser.BaseBranchId;
                    break;

                default:
                    _parentId = null; // Ensure _parentId is reset if user is not found
                    break;
            }

            return _parentId;
        }



        /// <summary>
        /// Retrieves a list of users by their IDs.
        /// </summary>
        /// <param name="context">The FlexApp context bridge used for repository initialization.</param>
        /// <param name="userIds">A list of user IDs to fetch.</param>
        /// <returns>A list of <see cref="ApplicationUser"/> entities.</returns>
        public async Task<List<ApplicationUser>> GetUsersByIdsAsync(FlexAppContextBridge context, List<string> userIds)
        {
            _repoFactory.Init(context);

            return await _repoFactory.GetRepo()
                                     .FindAll<ApplicationUser>()
                                     .ByTFlexIds(userIds)
                                     .ToListAsync();
        }
        /// <summary>
        /// Retrieves a list of users by their IDs.
        /// </summary>
        /// <param name="context">The FlexApp context bridge used for repository initialization.</param>
        /// <param name="userIds">A list of user IDs to fetch.</param>
        /// <returns>A list of <see cref="ApplicationUser"/> entities.</returns>
        public async Task<List<AgencyUser>> GetAgencyByUserIdsAsync(FlexAppContextBridge context, List<string> userIds)
        {
            _repoFactory.Init(context);

            return await _repoFactory.GetRepo()
                                     .FindAll<AgencyUser>()
                                     .ByAgencyUserCustomIds(userIds)
                                     .FlexInclude(x => x.Agency)
                                     .ToListAsync();
        }

        /// <summary>
        ///return a list of user assigned branch
        /// </summary>
        /// <param name="context">The FlexApp context bridge used for repository initialization.</param>
        /// <param name="userId">which user branch is to be retrieved</param>
        /// <returns>The branch as a string, or null if not applicable</returns>
        public async Task<List<string?>?> GetUserBranchByIdAsync(FlexAppContextBridge context, string userId)
        {
            _repoFactory.Init(context);

            var user = await _repoFactory.GetRepo().FindAll<ApplicationUser>()
                                    .Where(x => x.Id == userId)
                                    .FirstOrDefaultAsync();

            if (user is null)
                return null;

            if (user is AgencyUser)
            {
                return await _repoFactory.GetRepo().FindAll<AgencyUserScopeOfWork>()
                                        .Where(x => x.AgencyUserId == userId)
                                        .Select(s => s.Branch)
                                        .ToListAsync();
            }
            else
            {
                return await _repoFactory.GetRepo().FindAll<CompanyUserScopeOfWork>()
                                        .Where(x => x.CompanyUserId == userId)
                                        .Select(s => s.Branch)
                                        .ToListAsync();
            }
        }

        public async Task<List<UserLevelProjection>> GetQueueProjectionsByUserId(FlexAppContextBridge flexAppContext, string applicationUserId)
        {
            _repoFactory.Init(flexAppContext);

            return await _repoFactory.GetRepo()
                                     .FindAll<UserLevelProjection>()
                                     .Where(a => a.ApplicationUserId == applicationUserId)
                                     .ToListAsync();
        }
        public async Task<List<UserAttendanceLog>> GetFirstMobileLoginsOnDateAsync(FlexAppContextBridge context, DateTime loginDate)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo()
                .FindAll<UserAttendanceLog>()
                              .HasUserLoggedInOnDate(loginDate)
                              .IsMobileLogin()
                              .IsFirstLogin()
                              .FlexInclude(x => x.ApplicationUser)
                              .ToListAsync();

        }

        public async Task<List<UserAttendanceLog>> GetLastLogoutTimesForUsersAsync(
     FlexAppContextBridge context, List<string> userIds, DateTime reportDate)
        {
            _repoFactory.Init(context);

            var startOfDay = reportDate.Date;
            var endOfDay = startOfDay.AddDays(1);

            // Step 1: Get ApplicationUser details for all userIds
            var users = await _repoFactory
                .GetRepo()
                .FindAll<ApplicationUser>()
                .Where(u => userIds.Contains(u.Id))
                .ToListAsync();

            var userMap = users.ToDictionary(u => u.Id, u => u);

            // Step 2: Get actual logout entries for the date
            var logoutCandidates = await _repoFactory
                .GetRepo()
                .FindAll<UserAttendanceLog>()
                .ByUserAttendanceLogUserIds(userIds)
                .FlexInclude(x => x.ApplicationUser)
                .HasUserLoggedOutOnDate(reportDate)
                .ToListAsync();

            // Step 3: Get latest logout per user
            var actualLogouts = logoutCandidates
                .GroupBy(x => x.ApplicationUser.Id)
                .Select(g => g.OrderByDescending(x => x.LogOutTime).First())
                .ToDictionary(x => x.ApplicationUser.Id, x => x);

            // Step 4: Prepare synthetic entries for missing users
            foreach (var userId in userIds)
            {
                if (!actualLogouts.ContainsKey(userId) && userMap.TryGetValue(userId, out var user))
                {
                    actualLogouts[userId] = new UserAttendanceLog
                    {
                        ApplicationUser = user,
                        LogOutTime = startOfDay.AddHours(23).AddMinutes(59).AddSeconds(59),
                        IsSessionValid = false,
                        LogInTime = null,
                        LogOutLatitude = null,
                        LogOutLongitude = null,
                        // other default values if necessary
                    };
                }
            }

            return actualLogouts.Values.ToList();
        }




    }
}
