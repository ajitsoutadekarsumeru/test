namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoanAccountJSON : DomainModelBridge
    {
        public static IEnumerable<LoanAccountJSON> GetSeedData()
        {
            ICollection<LoanAccountJSON> seedData = new List<LoanAccountJSON>()
            {
                //add your object collection seed here:

                new LoanAccountJSON()
                {
                }
            };

            return seedData;
        }
    }
}