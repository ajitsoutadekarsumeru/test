namespace ENTiger.ENCollect;

public partial class HierarchyLevel : PersistenceModelBridge
{
    public static IEnumerable<HierarchyLevel> GetSeedData()
    {
        ICollection<HierarchyLevel> seedData = new List<HierarchyLevel>()
        {
            //add your object collection seed here:

            new HierarchyLevel()
            {
                
            }
        };
        return seedData;
    }
}
