using System.Collections.Generic;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CommunicationTrigger : DomainModelBridge
    {
        public static IEnumerable<CommunicationTrigger> GetSeedData()
        {
            ICollection<CommunicationTrigger> seedData = new List<CommunicationTrigger>()
            {
                //add your object collection seed here:

                new CommunicationTrigger()
                {
                    
                }

            };

            return seedData;
        }

    }
}
