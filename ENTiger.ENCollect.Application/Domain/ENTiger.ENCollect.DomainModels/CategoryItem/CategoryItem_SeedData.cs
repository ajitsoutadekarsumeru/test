namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CategoryItem : DomainModelBridge
    {
        public static IEnumerable<CategoryItem> GetSeedData()
        {
            ICollection<CategoryItem> seedData = new List<CategoryItem>()
            {
                //add your object collection seed here:

                new CategoryItem()
                {
                }
            };

            return seedData;
        }
    }
}