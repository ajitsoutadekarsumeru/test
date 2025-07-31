namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentHistory : DomainModelBridge
    {
        public static IEnumerable<TreatmentHistory> GetSeedData()
        {
            ICollection<TreatmentHistory> seedData = new List<TreatmentHistory>()
            {
                //add your object collection seed here:

                new TreatmentHistory()
                {
                }
            };

            return seedData;
        }
    }
}