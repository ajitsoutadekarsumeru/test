namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoanAccount : DomainModelBridge
    {
        public static IEnumerable<LoanAccount> GetSeedData()
        {
            ICollection<LoanAccount> seedData = new List<LoanAccount>()
            {
                //add your object collection seed here:

                new LoanAccount()
                {
                }
            };

            return seedData;
        }
    }
}