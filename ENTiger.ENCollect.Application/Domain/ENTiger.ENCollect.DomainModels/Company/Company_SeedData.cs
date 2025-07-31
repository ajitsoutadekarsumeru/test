namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Company : DomainModelBridge
    {
        public static IEnumerable<Company> GetSeedData()
        {
            ICollection<Company> seedData = new List<Company>()
            {
                //add your object collection seed here:

                new Company()
                {
                }
            };

            return seedData;
        }
    }
}