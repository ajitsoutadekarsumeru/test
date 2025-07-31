namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Collection : DomainModelBridge
    {
        public static IEnumerable<Collection> GetSeedData()
        {
            ICollection<Collection> seedData = new List<Collection>()
            {
                //add your object collection seed here:

                new Collection()
                {
                }
            };

            return seedData;
        }
    }
}