namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserCurrentLocationDetails : DomainModelBridge
    {
        public static IEnumerable<UserCurrentLocationDetails> GetSeedData()
        {
            ICollection<UserCurrentLocationDetails> seedData = new List<UserCurrentLocationDetails>()
            {
                //add your object collection seed here:

                new UserCurrentLocationDetails()
                {
                }
            };

            return seedData;
        }
    }
}