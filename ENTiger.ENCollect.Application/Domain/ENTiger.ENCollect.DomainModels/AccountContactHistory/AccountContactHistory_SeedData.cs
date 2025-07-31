using System.Collections.Generic;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AccountContactHistory : PersistenceModelBridge
    {
        public static IEnumerable<AccountContactHistory> GetSeedData()
        {
            ICollection<AccountContactHistory> seedData = new List<AccountContactHistory>()
            {
                //add your object collection seed here:

                new AccountContactHistory()
                {
                    
                }

            };

            return seedData;
        }

    }
}
