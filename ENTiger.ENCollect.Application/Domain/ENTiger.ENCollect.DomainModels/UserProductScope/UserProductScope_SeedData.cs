namespace ENTiger.ENCollect;
public partial class UserProductScope : PersistenceModelBridge
{
    public static IEnumerable<UserProductScope> GetSeedData()
    {
        ICollection<UserProductScope> seedData = new List<UserProductScope>()
        {
            //add your object collection seed here:

            new UserProductScope()
            {
            }
        };
        return seedData;
    }
}