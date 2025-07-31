using System.Collections.Generic;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class EnabledPermission : DomainModelBridge
    {
        public static IEnumerable<EnabledPermission> GetSeedData()
        {
            ICollection<EnabledPermission> seedData = new List<EnabledPermission>()
            {
                //add your object collection seed here:

                new EnabledPermission()
                {
                    
                }

            };

            return seedData;
        }

    }
}
