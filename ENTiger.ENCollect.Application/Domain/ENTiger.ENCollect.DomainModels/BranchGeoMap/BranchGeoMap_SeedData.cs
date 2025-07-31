namespace ENTiger.ENCollect;

public partial class BranchGeoMap : PersistenceModelBridge
{
    public static IEnumerable<BranchGeoMap> GetSeedData()
    {
        ICollection<BranchGeoMap> seedData = new List<BranchGeoMap>()
            {
                //add your object collection seed here:

                new BranchGeoMap()
                {
                }
            };
        return seedData;
    }
}
