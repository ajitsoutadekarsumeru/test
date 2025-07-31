namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class RoundRobinTreatment : DomainModelBridge
    {
        public static IEnumerable<RoundRobinTreatment> GetSeedData()
        {
            ICollection<RoundRobinTreatment> seedData = new List<RoundRobinTreatment>()
            {
                //add your object collection seed here:

                new RoundRobinTreatment()
                {
                }
            };

            return seedData;
        }
    }
}