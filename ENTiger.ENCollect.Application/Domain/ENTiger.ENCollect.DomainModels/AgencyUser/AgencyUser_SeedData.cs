namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class AgencyUser : ApplicationUser
    {
        public static IEnumerable<AgencyUser> GetSeedData()
        {
            ICollection<AgencyUser> seedData = new List<AgencyUser>()
            {
                //add your object collection seed here:

                new AgencyUser()
                {
                }
            };

            return seedData;
        }
    }
}