namespace ENTiger.ENCollect
{
    /// <summary>
    ///
    /// </summary>
    public partial class UserAttendanceLog : DomainModelBridge
    {
        public static IEnumerable<UserAttendanceLog> GetSeedData()
        {
            ICollection<UserAttendanceLog> seedData = new List<UserAttendanceLog>()
            {
                //add your object collection seed here:

                new UserAttendanceLog()
                {
                }
            };

            return seedData;
        }
    }
}