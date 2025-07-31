namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApplicationUser : PersonBridge
    {
        public static IEnumerable<ApplicationUser> GetSeedData()
        {
            ICollection<ApplicationUser> seedData = new List<ApplicationUser>()
            {
                //add your object collection seed here:

                new ApplicationUser()
                {
                }
            };

            return seedData;
        }
    }
}