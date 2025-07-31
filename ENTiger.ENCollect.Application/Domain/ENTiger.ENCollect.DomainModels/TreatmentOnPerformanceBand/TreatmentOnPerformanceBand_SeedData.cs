namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnPerformanceBand : DomainModelBridge
    {
        public static IEnumerable<TreatmentOnPerformanceBand> GetSeedData()
        {
            ICollection<TreatmentOnPerformanceBand> seedData = new List<TreatmentOnPerformanceBand>()
            {
                //add your object collection seed here:

                new TreatmentOnPerformanceBand()
                {
                }
            };

            return seedData;
        }
    }
}