namespace ENTiger.ENCollect;

public partial class UserGeoScope : PersistenceModelBridge
{
    public static IEnumerable<UserGeoScope> GetSeedData()
    {
        ICollection<UserGeoScope> seedData = new List<UserGeoScope>()
        {
            //add your object collection seed here:

            new UserGeoScope()
            {
            }
        };
        return seedData;
    }
}