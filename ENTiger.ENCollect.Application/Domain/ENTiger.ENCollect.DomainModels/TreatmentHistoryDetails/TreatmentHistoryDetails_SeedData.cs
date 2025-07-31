namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentHistoryDetails : DomainModelBridge
    {
        public static IEnumerable<TreatmentHistoryDetails> GetSeedData()
        {
            ICollection<TreatmentHistoryDetails> seedData = new List<TreatmentHistoryDetails>()
            {
                //add your object collection seed here:

                new TreatmentHistoryDetails()
                {
                }
            };

            return seedData;
        }
    }
}