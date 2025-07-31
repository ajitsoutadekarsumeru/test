using System.Collections.Generic;

namespace ENTiger.ENCollect
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CollectionUploadFile : DomainModelBridge
    {
        public static IEnumerable<CollectionUploadFile> GetSeedData()
        {
            ICollection<CollectionUploadFile> seedData = new List<CollectionUploadFile>()
            {
                //add your object collection seed here:

                new CollectionUploadFile()
                {
                    
                }

            };

            return seedData;
        }

    }
}
