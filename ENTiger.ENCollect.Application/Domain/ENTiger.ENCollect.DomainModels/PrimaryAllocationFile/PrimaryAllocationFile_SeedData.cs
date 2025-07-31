using System.Collections.Generic;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class PrimaryAllocationFile : DomainModelBridge
    {
        public static IEnumerable<PrimaryAllocationFile> GetSeedData()
        {
            ICollection<PrimaryAllocationFile> seedData = new List<PrimaryAllocationFile>()
            {
                //add your object collection seed here:

                new PrimaryAllocationFile()
                {
                }
            };

            return seedData;
        }
    }
}