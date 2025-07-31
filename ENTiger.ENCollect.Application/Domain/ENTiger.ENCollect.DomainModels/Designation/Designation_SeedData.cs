namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Designation : DomainModelBridge
    {
        public static IEnumerable<Designation> GetSeedData()
        {
            ICollection<Designation> seedData = new List<Designation>()
            {
                //add your object collection seed here:

                new Designation()
                {
                }
            };

            return seedData;
        }
    }
}