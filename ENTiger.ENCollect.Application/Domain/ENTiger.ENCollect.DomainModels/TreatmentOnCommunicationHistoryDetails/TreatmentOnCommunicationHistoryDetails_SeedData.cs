namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnCommunicationHistoryDetails : DomainModelBridge
    {
        public static IEnumerable<TreatmentOnCommunicationHistoryDetails> GetSeedData()
        {
            ICollection<TreatmentOnCommunicationHistoryDetails> seedData = new List<TreatmentOnCommunicationHistoryDetails>()
            {
                //add your object collection seed here:

                new TreatmentOnCommunicationHistoryDetails()
                {
                }
            };

            return seedData;
        }
    }
}