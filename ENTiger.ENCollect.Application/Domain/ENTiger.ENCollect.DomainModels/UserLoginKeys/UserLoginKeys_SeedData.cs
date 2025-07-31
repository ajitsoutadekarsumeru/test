namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserLoginKeys : DomainModelBridge
    {
        public static IEnumerable<UserLoginKeys> GetSeedData()
        {
            ICollection<UserLoginKeys> seedData = new List<UserLoginKeys>()
            {
                //add your object collection seed here:

                new UserLoginKeys()
                {
                }
            };

            return seedData;
        }
    }
}