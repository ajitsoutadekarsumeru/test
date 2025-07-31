namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserVerificationCodeTypes : DomainModelBridge
    {
        public static IEnumerable<UserVerificationCodeTypes> GetSeedData()
        {
            ICollection<UserVerificationCodeTypes> seedData = new List<UserVerificationCodeTypes>()
            {
                //add your object collection seed here:

                new UserVerificationCodeTypes()
                {
                }
            };

            return seedData;
        }
    }
}