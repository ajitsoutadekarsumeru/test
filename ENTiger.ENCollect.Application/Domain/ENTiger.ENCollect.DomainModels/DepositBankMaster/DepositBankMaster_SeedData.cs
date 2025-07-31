namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DepositBankMaster : DomainModelBridge
    {
        public static IEnumerable<DepositBankMaster> GetSeedData()
        {
            ICollection<DepositBankMaster> seedData = new List<DepositBankMaster>()
            {
                //add your object collection seed here:

                new DepositBankMaster()
                {
                }
            };

            return seedData;
        }
    }
}