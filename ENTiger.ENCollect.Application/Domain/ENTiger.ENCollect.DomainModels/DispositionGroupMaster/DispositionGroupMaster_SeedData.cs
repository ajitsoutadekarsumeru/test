namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DispositionGroupMaster : DomainModelBridge
    {
        public static IEnumerable<DispositionGroupMaster> GetSeedData()
        {
            ICollection<DispositionGroupMaster> seedData = new List<DispositionGroupMaster>()
            {
                //add your object collection seed here:

                new DispositionGroupMaster()
                {
                }
            };

            return seedData;
        }
    }
}