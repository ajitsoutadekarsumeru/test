namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class ApplicationUserDetails : DomainModelBridge
    {
        public static IEnumerable<ApplicationUserDetails> GetSeedData()
        {
            ICollection<ApplicationUserDetails> seedData = new List<ApplicationUserDetails>()
            {
                //add your object collection seed here:

                new ApplicationUserDetails()
                {
                }
            };

            return seedData;
        }
    }
}