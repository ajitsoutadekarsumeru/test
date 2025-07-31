namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class BankBranch : DomainModelBridge
    {
        public static IEnumerable<BankBranch> GetSeedData()
        {
            ICollection<BankBranch> seedData = new List<BankBranch>()
            {
                //add your object collection seed here:

                new BankBranch()
                {
                }
            };

            return seedData;
        }
    }
}