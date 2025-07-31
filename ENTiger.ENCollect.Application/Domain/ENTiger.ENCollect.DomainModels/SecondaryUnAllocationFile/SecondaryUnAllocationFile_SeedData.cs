namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class SecondaryUnAllocationFile : DomainModelBridge
    {
        public static IEnumerable<SecondaryUnAllocationFile> GetSeedData()
        {
            ICollection<SecondaryUnAllocationFile> seedData = new List<SecondaryUnAllocationFile>()
            {
                //add your object collection seed here:

                new SecondaryUnAllocationFile()
                {
                }
            };

            return seedData;
        }
    }
}