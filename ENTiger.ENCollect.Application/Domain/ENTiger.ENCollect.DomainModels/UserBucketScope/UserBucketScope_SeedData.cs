namespace ENTiger.ENCollect;

public partial class UserBucketScope : PersistenceModelBridge
{
    public static IEnumerable<UserBucketScope> GetSeedData()
    {
        ICollection<UserBucketScope> seedData = new List<UserBucketScope>()
        {
            //add your object collection seed here:

            new UserBucketScope()
            {
            }
        };
        return seedData;
    }
}