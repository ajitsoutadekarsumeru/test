using System.Collections.Generic;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TriggerDeliverySpec : DomainModelBridge
    {
        public static IEnumerable<TriggerDeliverySpec> GetSeedData()
        {
            ICollection<TriggerDeliverySpec> seedData = new List<TriggerDeliverySpec>()
            {
                //add your object collection seed here:

                new TriggerDeliverySpec()
                {
                    
                }

            };

            return seedData;
        }

    }
}
