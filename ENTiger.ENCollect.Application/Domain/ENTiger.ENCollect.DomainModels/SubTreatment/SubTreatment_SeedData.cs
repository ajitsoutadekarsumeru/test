namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class SubTreatment : DomainModelBridge
    {
        public static IEnumerable<SubTreatment> GetSeedData()
        {
            ICollection<SubTreatment> seedData = new List<SubTreatment>()
            {
                //add your object collection seed here:

                new SubTreatment()
                {
                }
            };

            return seedData;
        }
    }
}