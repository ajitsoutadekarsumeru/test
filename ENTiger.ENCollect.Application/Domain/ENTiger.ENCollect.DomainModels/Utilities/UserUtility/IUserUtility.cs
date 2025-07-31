namespace ENTiger.ENCollect;
public interface IUserUtility
{
    Task<string> ValidateUserStatus(ApplicationUser appUser, dynamic dto);

    Task InsertUserActivityDetails(string userId, string activityType, double? lat, double? lng, dynamic dto);

    Task<List<Permissions>> GetUserPermissions(ApplicationUser appUser, DtoBridge dto);

    Task<Dictionary<string, string>> GetClaims(DtoBridge dto);
}