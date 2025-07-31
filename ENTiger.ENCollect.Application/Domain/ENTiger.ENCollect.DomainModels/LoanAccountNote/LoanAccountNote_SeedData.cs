namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class LoanAccountNote : DomainModelBridge
    {
        public static IEnumerable<LoanAccountNote> GetSeedData()
        {
            ICollection<LoanAccountNote> seedData = new List<LoanAccountNote>()
            {
                //add your object collection seed here:

                new LoanAccountNote()
                {
                }
            };

            return seedData;
        }
    }
}