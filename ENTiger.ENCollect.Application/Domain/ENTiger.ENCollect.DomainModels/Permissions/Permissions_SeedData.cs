using System.Collections.Generic;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Permissions : DomainModelBridge
    {
        public static IEnumerable<Permissions> GetSeedData()
        {
            ICollection<Permissions> seedData = new List<Permissions>()
            {
                //add your object collection seed here:

                new Permissions()
                {
                    
                }

            };

            return seedData;
        }

    }
}
