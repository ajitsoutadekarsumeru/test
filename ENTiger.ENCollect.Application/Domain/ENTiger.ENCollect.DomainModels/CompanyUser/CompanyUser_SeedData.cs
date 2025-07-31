namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class CompanyUser : ApplicationUser
    {
        public static IEnumerable<CompanyUser> GetSeedData()
        {
            ICollection<CompanyUser> seedData = new List<CompanyUser>()
            {
                //add your object collection seed here:

                new CompanyUser()
                {
                }
            };

            return seedData;
        }
    }
}