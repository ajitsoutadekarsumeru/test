namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class Address : DomainModelBridge
    {
        public static IEnumerable<Address> GetSeedData()
        {
            ICollection<Address> seedData = new List<Address>()
            {
                //add your object collection seed here:

                new Address()
                {
                }
            };

            return seedData;
        }
    }
}