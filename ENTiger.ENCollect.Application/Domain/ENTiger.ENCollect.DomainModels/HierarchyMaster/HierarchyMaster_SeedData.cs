namespace ENTiger.ENCollect;

public partial class HierarchyMaster : PersistenceModelBridge
{
    public static IEnumerable<HierarchyMaster> GetSeedData()
    {
        ICollection<HierarchyMaster> seedData = new List<HierarchyMaster>()
        {
            //add your object collection seed here:

            new HierarchyMaster()
            {
                
            }
        };
        return seedData;
    }
}
