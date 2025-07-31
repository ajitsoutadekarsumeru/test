namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Sequence : DomainModelBridge
    {
        public static IEnumerable<Sequence> GetSeedData()
        {
            ICollection<Sequence> seedData = new List<Sequence>()
            {
                //add your object collection seed here:

                new Sequence()
                {
                }
            };

            return seedData;
        }
    }
}