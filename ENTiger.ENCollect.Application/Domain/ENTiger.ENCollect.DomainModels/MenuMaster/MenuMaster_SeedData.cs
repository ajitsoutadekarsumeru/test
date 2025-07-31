namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class MenuMaster : DomainModelBridge
    {
        public static IEnumerable<MenuMaster> GetSeedData()
        {
            ICollection<MenuMaster> seedData = new List<MenuMaster>()
            {
                //add your object collection seed here:

                new MenuMaster()
                {
                }
            };

            return seedData;
        }
    }
}