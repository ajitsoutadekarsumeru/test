namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Countries : DomainModelBridge
    {
        public static IEnumerable<Countries> GetSeedData()
        {
            ICollection<Countries> seedData = new List<Countries>()
            {
                //add your object collection seed here:

                new Countries()
                {
                }
            };

            return seedData;
        }
    }
}