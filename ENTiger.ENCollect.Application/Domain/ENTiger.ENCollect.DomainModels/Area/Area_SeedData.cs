namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Area : DomainModelBridge
    {
        public static IEnumerable<Area> GetSeedData()
        {
            ICollection<Area> seedData = new List<Area>()
            {
                //add your object collection seed here:

                new Area()
                {
                }
            };

            return seedData;
        }
    }
}