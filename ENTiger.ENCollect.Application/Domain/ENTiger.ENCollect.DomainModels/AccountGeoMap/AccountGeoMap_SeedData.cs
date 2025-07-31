namespace ENTiger.ENCollect
{
    public partial class AccountGeoMap : PersistenceModelBridge
    {
        public static IEnumerable<AccountGeoMap> GetSeedData()
        {
            ICollection<AccountGeoMap> seedData = new List<AccountGeoMap>()
            {
                //add your object collection seed here:

                new AccountGeoMap()
                {
                }
            };
            return seedData;
        }
    }
}