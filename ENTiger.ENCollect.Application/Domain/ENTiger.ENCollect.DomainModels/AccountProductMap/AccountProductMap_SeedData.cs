namespace ENTiger.ENCollect
{
    public partial class AccountProductMap : PersistenceModelBridge
    {
        public static IEnumerable<AccountProductMap> GetSeedData()
        {
            ICollection<AccountProductMap> seedData = new List<AccountProductMap>()
            {
                //add your object collection seed here:

                new AccountProductMap()
                {
                }
            };
            return seedData;
        }
    }
}