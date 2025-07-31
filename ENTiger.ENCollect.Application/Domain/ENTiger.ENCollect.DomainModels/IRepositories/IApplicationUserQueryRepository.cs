
namespace ENTiger.ENCollect
{
    public interface IApplicationUserQueryRepository
    {
        Task<string> GetParentIdByUserId(string userId, FlexAppContextBridge context);
        Task<List<ApplicationUser>> GetUsersByIdsAsync(FlexAppContextBridge context, List<string> userIds);
        Task<List<string?>?> GetUserBranchByIdAsync(FlexAppContextBridge context, string userId);
        Task<List<UserLevelProjection>> GetQueueProjectionsByUserId(FlexAppContextBridge flexAppContext, string applicationUserId);
        Task<List<UserAttendanceLog>> GetFirstMobileLoginsOnDateAsync(FlexAppContextBridge context, DateTime loginDate);
        Task<List<UserAttendanceLog>> GetLastLogoutTimesForUsersAsync(FlexAppContextBridge context, List<string> userIds, DateTime logoutDate);

        Task<List<AgencyUser>> GetAgencyByUserIdsAsync(FlexAppContextBridge flexAppContextBridge, List<string?> userIds);

    }
}
