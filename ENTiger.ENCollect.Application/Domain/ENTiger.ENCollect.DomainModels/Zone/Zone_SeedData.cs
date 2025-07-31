namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Zone : DomainModelBridge
    {
        public static IEnumerable<Zone> GetSeedData()
        {
            ICollection<Zone> seedData = new List<Zone>()
            {
                //add your object collection seed here:

                new Zone()
                {
                }
            };

            return seedData;
        }
    }
}