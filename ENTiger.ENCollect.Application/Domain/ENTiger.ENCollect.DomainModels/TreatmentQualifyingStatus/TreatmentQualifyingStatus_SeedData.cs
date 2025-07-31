namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentQualifyingStatus : DomainModelBridge
    {
        public static IEnumerable<TreatmentQualifyingStatus> GetSeedData()
        {
            ICollection<TreatmentQualifyingStatus> seedData = new List<TreatmentQualifyingStatus>()
            {
                //add your object collection seed here:

                new TreatmentQualifyingStatus()
                {
                }
            };

            return seedData;
        }
    }
}