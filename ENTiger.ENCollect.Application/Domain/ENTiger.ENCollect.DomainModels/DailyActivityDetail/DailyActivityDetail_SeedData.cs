namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class DailyActivityDetail : DomainModelBridge
    {
        public static IEnumerable<DailyActivityDetail> GetSeedData()
        {
            ICollection<DailyActivityDetail> seedData = new List<DailyActivityDetail>()
            {
                //add your object collection seed here:

                new DailyActivityDetail()
                {
                }
            };

            return seedData;
        }
    }
}