namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CollectionBatch : DomainModelBridge
    {
        public static IEnumerable<CollectionBatch> GetSeedData()
        {
            ICollection<CollectionBatch> seedData = new List<CollectionBatch>()
            {
                //add your object collection seed here:

                new CollectionBatch()
                {
                }
            };

            return seedData;
        }
    }
}