namespace ENTiger.ENCollect
{
    public interface IGeoTagRepository
    {
        Task<List<GeoTagDetails>> GetGeoTagsForUsersAsync(FlexAppContextBridge context, List<string> userIds, DateTime reportDate);
    }
}
