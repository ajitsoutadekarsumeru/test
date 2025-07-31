namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserPerformanceBand : DomainModelBridge
    {
        public static IEnumerable<UserPerformanceBand> GetSeedData()
        {
            ICollection<UserPerformanceBand> seedData = new List<UserPerformanceBand>()
            {
                //add your object collection seed here:

                new UserPerformanceBand()
                {
                }
            };

            return seedData;
        }
    }
}