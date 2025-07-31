namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class TreatmentDesignation : DomainModelBridge
    {
        public static IEnumerable<TreatmentDesignation> GetSeedData()
        {
            ICollection<TreatmentDesignation> seedData = new List<TreatmentDesignation>()
            {
                //add your object collection seed here:

                new TreatmentDesignation()
                {
                }
            };

            return seedData;
        }
    }
}