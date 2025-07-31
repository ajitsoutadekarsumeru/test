namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnCommunication : DomainModelBridge
    {
        public static IEnumerable<TreatmentOnCommunication> GetSeedData()
        {
            ICollection<TreatmentOnCommunication> seedData = new List<TreatmentOnCommunication>()
            {
                //add your object collection seed here:

                new TreatmentOnCommunication()
                {
                }
            };

            return seedData;
        }
    }
}