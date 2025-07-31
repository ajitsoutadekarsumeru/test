namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Cheque : DomainModelBridge
    {
        public static IEnumerable<Cheque> GetSeedData()
        {
            ICollection<Cheque> seedData = new List<Cheque>()
            {
                //add your object collection seed here:

                new Cheque()
                {
                }
            };

            return seedData;
        }
    }
}