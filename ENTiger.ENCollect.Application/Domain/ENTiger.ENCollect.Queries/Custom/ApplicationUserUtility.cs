using Microsoft.EntityFrameworkCore;

namespace ENTiger.ENCollect
{
    public class ApplicationUserUtility : IApplicationUserUtility
    {
        protected readonly IRepoFactory _repoFactory;

        public ApplicationUserUtility(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }

        public string GetApplicationUserId(string authUserId, FlexAppContextBridge context)
        {
            _repoFactory.Init(context);
            return _repoFactory.GetRepo().FindAll<ApplicationUser>().Where(x => x.UserId == authUserId).Select(x => x.Id).FirstOrDefault() ?? "";
        }

        public async Task<string> GetUserAttendance(string userId, string token, FlexAppContextBridge context)
        {
            _repoFactory.Init(context);
            return await _repoFactory.GetRepo().FindAll<UserAttendanceLog>()
                                .Where(x => x.ApplicationUserId == userId && 
                                            x.SessionId == token && 
                                            x.IsSessionValid == true)
                                .Select(x => x.Id)
                                .FirstOrDefaultAsync() ?? "";
        }
    }
}