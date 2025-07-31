namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UsersUpdateFile : DomainModelBridge
    {
        public static IEnumerable<UsersUpdateFile> GetSeedData()
        {
            ICollection<UsersUpdateFile> seedData = new List<UsersUpdateFile>()
            {
                //add your object collection seed here:

                new UsersUpdateFile()
                {
                }
            };

            return seedData;
        }
    }
}