namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AllocationDownload : DomainModelBridge
    {
        public static IEnumerable<AllocationDownload> GetSeedData()
        {
            ICollection<AllocationDownload> seedData = new List<AllocationDownload>()
            {
                //add your object collection seed here:

                new AllocationDownload()
                {
                }
            };

            return seedData;
        }
    }
}