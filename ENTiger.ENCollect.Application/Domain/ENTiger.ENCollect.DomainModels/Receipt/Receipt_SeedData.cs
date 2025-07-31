namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Receipt : DomainModelBridge
    {
        public static IEnumerable<Receipt> GetSeedData()
        {
            ICollection<Receipt> seedData = new List<Receipt>()
            {
                //add your object collection seed here:

                new Receipt()
                {
                }
            };

            return seedData;
        }
    }
}