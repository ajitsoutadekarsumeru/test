namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnPOS : DomainModelBridge
    {
        public static IEnumerable<TreatmentOnPOS> GetSeedData()
        {
            ICollection<TreatmentOnPOS> seedData = new List<TreatmentOnPOS>()
            {
                //add your object collection seed here:

                new TreatmentOnPOS()
                {
                }
            };

            return seedData;
        }
    }
}