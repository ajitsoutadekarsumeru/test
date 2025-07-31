namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserSearchCriteria : DomainModelBridge
    {
        public static IEnumerable<UserSearchCriteria> GetSeedData()
        {
            ICollection<UserSearchCriteria> seedData = new List<UserSearchCriteria>()
            {
                //add your object collection seed here:

                new UserSearchCriteria()
                {
                }
            };

            return seedData;
        }
    }
}