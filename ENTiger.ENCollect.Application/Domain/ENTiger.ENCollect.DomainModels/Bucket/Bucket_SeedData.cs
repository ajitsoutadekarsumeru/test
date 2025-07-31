namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Bucket : DomainModelBridge
    {
        public static IEnumerable<Bucket> GetSeedData()
        {
            ICollection<Bucket> seedData = new List<Bucket>()
            {
                //add your object collection seed here:

                new Bucket()
                {
                }
            };

            return seedData;
        }
    }
}