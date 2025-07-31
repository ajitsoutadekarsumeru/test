
namespace ENTiger.ENCollect
{
    public partial class CustomerConsent : DomainModelBridge
    {
        public static IEnumerable<CustomerConsent> GetSeedData()
        {
            ICollection<CustomerConsent> seedData = new List<CustomerConsent>()
            {
                //add your object collection seed here:

                new CustomerConsent()
                {
                    
                }

            };

            return seedData;
        }

    }
}
