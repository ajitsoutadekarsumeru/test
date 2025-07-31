namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserAttendanceDetail : DomainModelBridge
    {
        public static IEnumerable<UserAttendanceDetail> GetSeedData()
        {
            ICollection<UserAttendanceDetail> seedData = new List<UserAttendanceDetail>()
            {
                //add your object collection seed here:

                new UserAttendanceDetail()
                {
                }
            };

            return seedData;
        }
    }
}