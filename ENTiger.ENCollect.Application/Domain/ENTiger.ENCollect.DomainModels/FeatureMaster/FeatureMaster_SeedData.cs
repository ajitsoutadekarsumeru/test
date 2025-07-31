namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class FeatureMaster : DomainModelBridge
    {
        public static IEnumerable<FeatureMaster> GetSeedData()
        {
            ICollection<FeatureMaster> seedData = new List<FeatureMaster>()
            {
                //add your object collection seed here:

                new FeatureMaster()
                {
                }
            };

            return seedData;
        }
    }
}