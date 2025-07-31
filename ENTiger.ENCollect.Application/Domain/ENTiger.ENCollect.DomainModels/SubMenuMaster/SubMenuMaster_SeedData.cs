namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class SubMenuMaster : DomainModelBridge
    {
        public static IEnumerable<SubMenuMaster> GetSeedData()
        {
            ICollection<SubMenuMaster> seedData = new List<SubMenuMaster>()
            {
                //add your object collection seed here:

                new SubMenuMaster()
                {
                }
            };

            return seedData;
        }
    }
}