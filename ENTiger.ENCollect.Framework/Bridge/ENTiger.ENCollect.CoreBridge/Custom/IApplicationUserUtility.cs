using System.Threading.Tasks;

namespace ENTiger.ENCollect
{
    public interface IApplicationUserUtility
    {
        string GetApplicationUserId(string authUserId, FlexAppContextBridge context);

        Task<string> GetUserAttendance(string userId, string token, FlexAppContextBridge context);
    }
}