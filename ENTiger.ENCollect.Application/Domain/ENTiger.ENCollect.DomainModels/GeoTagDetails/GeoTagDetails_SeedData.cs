using System.Collections.Generic;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class GeoTagDetails : DomainModelBridge
    {
        public static IEnumerable<GeoTagDetails> GetSeedData()
        {
            ICollection<GeoTagDetails> seedData = new List<GeoTagDetails>()
            {
                //add your object collection seed here:

                new GeoTagDetails()
                {
                }
            };

            return seedData;
        }
    }
}