namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserPersonaMaster : DomainModelBridge
    {
        public static IEnumerable<UserPersonaMaster> GetSeedData()
        {
            ICollection<UserPersonaMaster> seedData = new List<UserPersonaMaster>()
            {
                //add your object collection seed here:

                new UserPersonaMaster()
                {
                }
            };

            return seedData;
        }
    }
}