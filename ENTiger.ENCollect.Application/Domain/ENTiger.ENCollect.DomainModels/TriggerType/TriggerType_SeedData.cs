using System.Collections.Generic;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TriggerType : DomainModelBridge
    {
        public static IEnumerable<TriggerType> GetSeedData()
        {
            ICollection<TriggerType> seedData = new List<TriggerType>()
            {
                //add your object collection seed here:

                new TriggerType()
                {
                    
                }

            };

            return seedData;
        }

    }
}
