using System.Collections.Generic;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Settlement : DomainModelBridge
    {
        public static IEnumerable<Settlement> GetSeedData()
        {
            ICollection<Settlement> seedData = new List<Settlement>()
            {
                //add your object collection seed here:

                new Settlement()
                {
                    
                }

            };

            return seedData;
        }

    }
}
