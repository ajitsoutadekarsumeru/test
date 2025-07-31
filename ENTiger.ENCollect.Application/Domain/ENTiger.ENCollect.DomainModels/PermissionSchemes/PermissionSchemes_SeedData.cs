using System.Collections.Generic;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PermissionSchemes : DomainModelBridge
    {
        public static IEnumerable<PermissionSchemes> GetSeedData()
        {
            ICollection<PermissionSchemes> seedData = new List<PermissionSchemes>()
            {
                //add your object collection seed here:

                new PermissionSchemes()
                {
                    
                }

            };

            return seedData;
        }

    }
}
