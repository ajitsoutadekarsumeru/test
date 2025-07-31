namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryAllocationFile : DomainModelBridge
    {
        public static IEnumerable<SecondaryAllocationFile> GetSeedData()
        {
            ICollection<SecondaryAllocationFile> seedData = new List<SecondaryAllocationFile>()
            {
                //add your object collection seed here:

                new SecondaryAllocationFile()
                {
                }
            };

            return seedData;
        }
    }
}