namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnUpdateTrail : DomainModelBridge
    {
        public static IEnumerable<TreatmentOnUpdateTrail> GetSeedData()
        {
            ICollection<TreatmentOnUpdateTrail> seedData = new List<TreatmentOnUpdateTrail>()
            {
                //add your object collection seed here:

                new TreatmentOnUpdateTrail()
                {
                }
            };

            return seedData;
        }
    }
}