namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CategoryMaster : DomainModelBridge
    {
        public static IEnumerable<CategoryMaster> GetSeedData()
        {
            ICollection<CategoryMaster> seedData = new List<CategoryMaster>()
            {
                //add your object collection seed here:

                new CategoryMaster()
                {
                }
            };

            return seedData;
        }
    }
}