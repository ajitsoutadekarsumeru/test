namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class GeoMaster : DomainModelBridge
    {
        public static IEnumerable<GeoMaster> GetSeedData()
        {
            ICollection<GeoMaster> seedData = new List<GeoMaster>()
            {
                //add your object collection seed here:

                new GeoMaster()
                {
                }
            };

            return seedData;
        }
    }
}