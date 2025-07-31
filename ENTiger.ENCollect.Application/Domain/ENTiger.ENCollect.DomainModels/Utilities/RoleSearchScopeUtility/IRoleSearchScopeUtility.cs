namespace ENTiger.ENCollect
{
    public interface IRoleSearchScopeUtility
    {
        Task<string> GetRoleScopeInfo(string userId);
    }
}
