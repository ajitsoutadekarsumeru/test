namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Agency : ApplicationOrg
    {
        public static IEnumerable<Agency> GetSeedData()
        {
            ICollection<Agency> seedData = new List<Agency>()
            {
                //add your object collection seed here:

                new Agency()
                {
                }
            };

            return seedData;
        }
    }
}