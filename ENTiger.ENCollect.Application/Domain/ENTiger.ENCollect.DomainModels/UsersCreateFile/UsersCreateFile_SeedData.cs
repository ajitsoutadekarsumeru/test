namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UsersCreateFile : DomainModelBridge
    {
        public static IEnumerable<UsersCreateFile> GetSeedData()
        {
            ICollection<UsersCreateFile> seedData = new List<UsersCreateFile>()
            {
                //add your object collection seed here:

                new UsersCreateFile()
                {
                }
            };

            return seedData;
        }
    }
}