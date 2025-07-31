namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyCategory : DomainModelBridge
    {
        public static IEnumerable<AgencyCategory> GetSeedData()
        {
            ICollection<AgencyCategory> seedData = new List<AgencyCategory>()
            {
                //add your object collection seed here:

                new AgencyCategory()
                {
                }
            };

            return seedData;
        }
    }
}