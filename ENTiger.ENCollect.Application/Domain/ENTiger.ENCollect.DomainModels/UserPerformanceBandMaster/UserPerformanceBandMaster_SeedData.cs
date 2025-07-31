namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserPerformanceBandMaster : DomainModelBridge
    {
        public static IEnumerable<UserPerformanceBandMaster> GetSeedData()
        {
            ICollection<UserPerformanceBandMaster> seedData = new List<UserPerformanceBandMaster>()
            {
                //add your object collection seed here:

                new UserPerformanceBandMaster()
                {
                }
            };

            return seedData;
        }
    }
}