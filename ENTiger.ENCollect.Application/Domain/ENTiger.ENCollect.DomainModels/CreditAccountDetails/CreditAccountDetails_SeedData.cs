namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CreditAccountDetails : DomainModelBridge
    {
        public static IEnumerable<CreditAccountDetails> GetSeedData()
        {
            ICollection<CreditAccountDetails> seedData = new List<CreditAccountDetails>()
            {
                //add your object collection seed here:

                new CreditAccountDetails()
                {
                }
            };

            return seedData;
        }
    }
}