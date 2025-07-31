namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentAndSegmentMapping : DomainModelBridge
    {
        public static IEnumerable<TreatmentAndSegmentMapping> GetSeedData()
        {
            ICollection<TreatmentAndSegmentMapping> seedData = new List<TreatmentAndSegmentMapping>()
            {
                //add your object collection seed here:

                new TreatmentAndSegmentMapping()
                {
                }
            };

            return seedData;
        }
    }
}