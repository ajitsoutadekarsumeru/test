namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Regions : DomainModelBridge
    {
        public static IEnumerable<Regions> GetSeedData()
        {
            ICollection<Regions> seedData = new List<Regions>()
            {
                //add your object collection seed here:

                new Regions()
                {
                }
            };

            return seedData;
        }
    }
}