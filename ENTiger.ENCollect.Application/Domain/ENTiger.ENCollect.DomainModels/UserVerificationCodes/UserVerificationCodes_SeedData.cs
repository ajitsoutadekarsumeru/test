namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserVerificationCodes : DomainModelBridge
    {
        public static IEnumerable<UserVerificationCodes> GetSeedData()
        {
            ICollection<UserVerificationCodes> seedData = new List<UserVerificationCodes>()
            {
                //add your object collection seed here:

                new UserVerificationCodes()
                {
                }
            };

            return seedData;
        }
    }
}