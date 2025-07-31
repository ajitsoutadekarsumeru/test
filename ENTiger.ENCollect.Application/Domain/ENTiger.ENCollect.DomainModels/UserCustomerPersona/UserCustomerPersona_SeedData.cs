namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserCustomerPersona : DomainModelBridge
    {
        public static IEnumerable<UserCustomerPersona> GetSeedData()
        {
            ICollection<UserCustomerPersona> seedData = new List<UserCustomerPersona>()
            {
                //add your object collection seed here:

                new UserCustomerPersona()
                {
                }
            };

            return seedData;
        }
    }
}