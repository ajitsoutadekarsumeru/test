namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class BankAccountType : DomainModelBridge
    {
        public static IEnumerable<BankAccountType> GetSeedData()
        {
            ICollection<BankAccountType> seedData = new List<BankAccountType>()
            {
                //add your object collection seed here:

                new BankAccountType()
                {
                }
            };

            return seedData;
        }
    }
}