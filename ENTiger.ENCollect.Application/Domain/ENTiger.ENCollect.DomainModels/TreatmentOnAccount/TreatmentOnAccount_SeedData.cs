namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentOnAccount : DomainModelBridge
    {
        public static IEnumerable<TreatmentOnAccount> GetSeedData()
        {
            ICollection<TreatmentOnAccount> seedData = new List<TreatmentOnAccount>()
            {
                //add your object collection seed here:

                new TreatmentOnAccount()
                {
                }
            };

            return seedData;
        }
    }
}