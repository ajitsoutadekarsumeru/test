namespace ENTiger.ENCollect
{
    public partial class IdConfigMaster_SeedData : DomainModelBridge
    {
        public static IEnumerable<IdConfigMaster> GetSeedData()
        {
            ICollection<IdConfigMaster> seedData = new List<IdConfigMaster>()
            {
                //add your object collection seed here:

                new IdConfigMaster()
                {
                }
            };

            return seedData;
        }
    }
}