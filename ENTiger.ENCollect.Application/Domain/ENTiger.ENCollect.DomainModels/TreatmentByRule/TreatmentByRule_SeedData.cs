namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentByRule : DomainModelBridge
    {
        public static IEnumerable<TreatmentByRule> GetSeedData()
        {
            ICollection<TreatmentByRule> seedData = new List<TreatmentByRule>()
            {
                //add your object collection seed here:

                new TreatmentByRule()
                {
                }
            };

            return seedData;
        }
    }
}