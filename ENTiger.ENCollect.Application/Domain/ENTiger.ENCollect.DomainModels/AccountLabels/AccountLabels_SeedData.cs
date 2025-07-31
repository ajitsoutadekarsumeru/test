namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AccountLabels : DomainModelBridge
    {
        public static IEnumerable<AccountLabels> GetSeedData()
        {
            ICollection<AccountLabels> seedData = new List<AccountLabels>()
            {
                //add your object collection seed here:

                new AccountLabels()
                {
                }
            };

            return seedData;
        }
    }
}