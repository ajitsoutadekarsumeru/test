using System.Collections.Generic;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Bank : DomainModelBridge
    {
        public static IEnumerable<Bank> GetSeedData()
        {
            ICollection<Bank> seedData = new List<Bank>()
            {
                //add your object collection seed here:

                new Bank()
                {
                }
            };

            return seedData;
        }
    }
}