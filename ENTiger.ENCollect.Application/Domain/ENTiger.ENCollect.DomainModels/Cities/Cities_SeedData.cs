namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Cities : DomainModelBridge
    {
        public static IEnumerable<Cities> GetSeedData()
        {
            ICollection<Cities> seedData = new List<Cities>()
            {
                //add your object collection seed here:

                new Cities()
                {
                }
            };

            return seedData;
        }
    }
}