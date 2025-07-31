namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class ActionMaster : DomainModelBridge
    {
        public static IEnumerable<ActionMaster> GetSeedData()
        {
            ICollection<ActionMaster> seedData = new List<ActionMaster>()
            {
                //add your object collection seed here:

                new ActionMaster()
                {
                }
            };

            return seedData;
        }
    }
}