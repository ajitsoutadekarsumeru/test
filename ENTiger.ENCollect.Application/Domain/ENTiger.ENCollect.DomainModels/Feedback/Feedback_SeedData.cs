namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Feedback : DomainModelBridge
    {
        public static IEnumerable<Feedback> GetSeedData()
        {
            ICollection<Feedback> seedData = new List<Feedback>()
            {
                //add your object collection seed here:

                new Feedback()
                {
                }
            };

            return seedData;
        }
    }
}