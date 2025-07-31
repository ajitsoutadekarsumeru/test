namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class PrimaryUnAllocationFile : DomainModelBridge
    {
        public static IEnumerable<PrimaryUnAllocationFile> GetSeedData()
        {
            ICollection<PrimaryUnAllocationFile> seedData = new List<PrimaryUnAllocationFile>()
            {
                //add your object collection seed here:

                new PrimaryUnAllocationFile()
                {
                }
            };

            return seedData;
        }
    }
}