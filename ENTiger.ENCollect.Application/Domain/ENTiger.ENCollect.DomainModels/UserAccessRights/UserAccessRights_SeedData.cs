using Sumeru.Flex;

namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserAccessRights : AccessRight
    {
        public static IEnumerable<UserAccessRights> GetSeedData()
        {
            ICollection<UserAccessRights> seedData = new List<UserAccessRights>()
            {
                //add your object collection seed here:

                new UserAccessRights()
                {
                }
            };
            return seedData;
        }
    }
}