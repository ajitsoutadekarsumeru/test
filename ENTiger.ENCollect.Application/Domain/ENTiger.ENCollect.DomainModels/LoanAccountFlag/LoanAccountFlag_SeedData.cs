namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoanAccountFlag : DomainModelBridge
    {
        public static IEnumerable<LoanAccountFlag> GetSeedData()
        {
            ICollection<LoanAccountFlag> seedData = new List<LoanAccountFlag>()
            {
                //add your object collection seed here:

                new LoanAccountFlag()
                {
                }
            };

            return seedData;
        }
    }
}