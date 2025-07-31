namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentUpdateIntermediate : DomainModelBridge
    {
        public static IEnumerable<TreatmentUpdateIntermediate> GetSeedData()
        {
            ICollection<TreatmentUpdateIntermediate> seedData = new List<TreatmentUpdateIntermediate>()
            {
                //add your object collection seed here:

                new TreatmentUpdateIntermediate()
                {
                }
            };

            return seedData;
        }
    }
}