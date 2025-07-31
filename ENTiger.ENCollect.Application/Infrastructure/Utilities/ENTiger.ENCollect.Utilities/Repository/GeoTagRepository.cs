using Microsoft.EntityFrameworkCore;
using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// Repository for accessing geo-tag data for users.
    /// </summary>
    public class GeoTagRepository : IGeoTagRepository
    {
        private readonly IRepoFactory _repoFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeoTagRepository"/> class.
        /// </summary>
        /// <param name="repoFactory">The repository factory instance used to access the data store.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="repoFactory"/> is null.</exception>
        public GeoTagRepository(IRepoFactory repoFactory)
        {
            _repoFactory = repoFactory;
        }

        /// <summary>
        /// Gets geo-tag details for a specific user on a given report date.
        /// </summary>
        /// <param name="context">The Flex application context bridge.</param>
        /// <param name="userId">The ID of the user for whom geo-tags are to be fetched.</param>
        /// <param name="reportDate">The date for which the geo-tags are required.</param>
        /// <returns>A list of <see cref="GeoTagDetails"/> objects matching the specified criteria.</returns>
        public async Task<List<GeoTagDetails>> GetGeoTagsForUsersAsync(
            FlexAppContextBridge context,
            List<string> userIds,
            DateTime reportDate)
        {
            _repoFactory.Init(context);
            return await _repoFactory
                .GetRepo()
                .FindAll<GeoTagDetails>()
                .ByDate(reportDate)
                .ByGeoTagUsers(userIds)                
                .ByReceiptOrTrailTransactionType()
                .IsMobileTransaction()
                .FlexInclude(x => x.ApplicationUser)
                .FlexInclude(x => x.Account)
                .OrderBy(x => x.ApplicationUserId)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

        }
    }
}
