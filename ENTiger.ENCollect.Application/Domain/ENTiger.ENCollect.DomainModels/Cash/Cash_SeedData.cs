namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Cash : DomainModelBridge
    {
        public static IEnumerable<Cash> GetSeedData()
        {
            ICollection<Cash> seedData = new List<Cash>()
            {
                //add your object collection seed here:

                new Cash()
                {
                }
            };

            return seedData;
        }
    }
}