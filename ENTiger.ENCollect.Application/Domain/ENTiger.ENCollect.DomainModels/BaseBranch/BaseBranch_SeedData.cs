namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class BaseBranch : ApplicationOrg
    {
        public static IEnumerable<BaseBranch> GetSeedData()
        {
            ICollection<BaseBranch> seedData = new List<BaseBranch>()
            {
                //add your object collection seed here:

                new BaseBranch()
                {
                }
            };

            return seedData;
        }
    }
}